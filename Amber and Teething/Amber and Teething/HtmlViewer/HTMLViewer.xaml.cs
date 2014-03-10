using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Documents;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.IO;
using Amber_and_Teething.HtmlViewer;

namespace Amber_and_Teething.HtmlViewer
{
    [ContentPropertyAttribute("Html")]
    public partial class HTMLViewer : UserControl
    {
        public HTMLViewer()
        {
            InitializeComponent();
            var hor = new Binding("HorizontalScrollBarVisibility");
            hor.Mode = BindingMode.TwoWay;
            hor.Source = Scroll;
            SetBinding(HorizontalScrollBarVisibilityProperty,hor);
            var ver = new Binding("VerticalScrollBarVisibility");
            ver.Mode = BindingMode.TwoWay;
            ver.Source = Scroll;
            SetBinding(VerticalScrollBarVisibilityProperty, ver);
        }

        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = ScrollViewer.VerticalScrollBarVisibilityProperty;
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = ScrollViewer.HorizontalScrollBarVisibilityProperty;


        [System.ComponentModel.Category("Common")]
        [System.ComponentModel.DefaultValue("")]
        public string Html
        {
            get
            {
                return (string)GetValue(HtmlProperty);
            }
            set
            {
                SetValue(HtmlProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for HTML.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.Register("Html", typeof(string), typeof(HTMLViewer), new PropertyMetadata("", (d, e) =>
            {
                HTMLViewer tb = d as HTMLViewer;
                tb.OnHtmlPropertyChanged(e);
            }));

        private void OnHtmlPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem((x) =>
            {
                lock (this)
                {
                    if (string.IsNullOrWhiteSpace((string)e.NewValue))
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            LayoutRoot.Children.Clear();
                        });
                        return;
                    }
                    var xamlXml = HtmlToXamlConverter.ConvertHtmlToXamlInternal((string)e.NewValue, false);
                    devideParagraphs(xamlXml);
                    foreach (var hyperlink in xamlXml.Descendants(XName.Get(HtmlToXamlConverter.Xaml_Hyperlink, HtmlToXamlConverter._xamlNamespace)))
                    {
                        if (hyperlink.Attribute(HtmlToXamlConverter.Xaml_Foreground) == null)
                        {
                            hyperlink.SetAttributeValue(XName.Get(HtmlToXamlConverter.Xaml_Foreground, HtmlToXamlConverter._xamlNamespace), @"Transparent");
                        }
                    }
                    Dispatcher.BeginInvoke(() =>
                    {
                        var section = System.Windows.Markup.XamlReader.Load(xamlXml.ToString()) as System.Windows.Documents.Section;
                        LayoutRoot.Children.Clear();
                        Scroll.ScrollToVerticalOffset(0);
                        var count = section.Blocks.Count;

                        Block[] array = new Block[count];
                        section.Blocks.CopyTo(array, 0);
                        section.Blocks.Clear();

                        foreach (var block in array)
                        {
                            RichTextBox rtb = new RichTextBox();
                            Binding b = new Binding("FontSize");
                            b.Source = this;
                            rtb.SetBinding(RichTextBox.FontSizeProperty, b);
                            rtb.Blocks.Add(block);
                            b = new Binding("Foreground");
                            b.Source = this;
                            rtb.SetBinding(RichTextBox.ForegroundProperty, b);
                            LayoutRoot.Children.Add(rtb);
                            var p = block as Paragraph;
                            if (p != null)
                            {
                                postParseInlinesSettings(p.Inlines);
                            }
                        }
                    });
                }
            });            
        }

        // divide paragraphs by lineBreaks. It lets to show long paragraph in different rtb'es withot any visual issues
        private void devideParagraphs(XElement element)
        {
            var breaks = element.Descendants(XName.Get(HtmlToXamlConverter.Xaml_LineBreak, HtmlToXamlConverter._xamlNamespace)).ToArray();
            foreach ( var br in breaks)
            {
                if (br != null && br.ElementsAfterSelf().Count() > 0)
                {
                    if (br.NextNode.GetType() == typeof(XElement))
                    {
                        if ((br.NextNode as XElement).Name.LocalName == HtmlToXamlConverter.Xaml_LineBreak)
                        {
                            continue;
                        }
                    }
                    XElement par = new XElement(br.Parent);
                    par.SetValue("");
                    var nodes = br.NodesAfterSelf().ToArray();
                    foreach (var node in nodes)
                    {
                        node.Remove();
                    }
                    par.Add(nodes);
                    br.Parent.AddAfterSelf(par);
                    br.Remove();
                }
            }
            var sections = element.Descendants(XName.Get(HtmlToXamlConverter.Xaml_Section, HtmlToXamlConverter._xamlNamespace)).ToArray();
            foreach (var section in sections)
            {
                section.AddAfterSelf(section.Nodes());
                section.Remove();
            }
        }


        // no event can by added in xamlParser
        void postParseInlinesSettings(InlineCollection collection)
        {
            foreach (var il in collection)
            {
                var h = il as Hyperlink;
                if (h != null)
                {
                    h.CommandParameter = h.NavigateUri;

                    if (h.Foreground.GetType() == typeof(SolidColorBrush) && ((SolidColorBrush)h.Foreground).Color == Colors.Transparent)
                    {
                        h.Foreground = this.Foreground;
                    }

                    if (NavigaionPolitic == HTMLTextBoxHyperlynkNavigaionPolitic.Automatic)
                    {
                        h.TargetName = "_blank";
                    }
                    else
                    {
                        h.NavigateUri = null;
                    }
                    h.Click += new RoutedEventHandler(hyperlink_Click);
                    postParseInlinesSettings(h.Inlines);
                    continue;
                }
                var ui = il as InlineUIContainer;
                if (ui != null)
                {
                    (ui.Child as Button).Template = null;
                    (ui.Child as Button).Click += new RoutedEventHandler(HTMLViewer_Click);
                    var im = (ui.Child as Button).Content as Image;
                    im.ImageOpened += new EventHandler<RoutedEventArgs>(HTMLTextBox_ImageOpened);
                    var source = im.Source as BitmapImage;
                    // support for base64 encoded images
                    if (source == null && !string.IsNullOrEmpty(im.Name))
                    {
                        var base64string = im.Name;
                        byte[] fileBytes = Convert.FromBase64String(base64string);
                        source = new BitmapImage();
                        using (MemoryStream ms = new MemoryStream(fileBytes, 0, fileBytes.Length))
                        {
                            ms.Write(fileBytes, 0, fileBytes.Length);
                            source.CreateOptions = BitmapCreateOptions.None;
                            source.SetSource(ms);
                        }
                        im.Source = source;
                    }
                    if (source.PixelWidth > 0 && ActualWidth > 24 && double.IsNaN(im.Width))
                    {
                        var width = (double)ActualWidth - 24;
                        if (source.PixelWidth < width)
                            width = source.PixelWidth;
                        if (im.Width < width && im.Width != 0)
                            width = im.Width;

                        im.Width = width;
                        im.Height = source.PixelHeight * width / source.PixelWidth;
                    }

                    continue;
                }
                var span = il as Span;
                if (span != null)
                {
                    postParseInlinesSettings(span.Inlines);
                }
            }
        }

        void HTMLViewer_Click(object sender, RoutedEventArgs e)
        {
            if (ImageClick != null)
            {
                var im =(sender as Button).Content as Image;
                ImageClick(im, new ImageClickEventArgs(im.Source));
            }
        }

        void HTMLTextBox_ImageOpened(object sender, RoutedEventArgs e)
        {
            var im = sender as Image;
            var source = im.Source as BitmapImage;
            if (source.PixelWidth > 0 && ActualWidth > 24 && double.IsNaN(im.Width))
            {
                var width = (double)ActualWidth - 24;
                if (source.PixelWidth < width)
                    width = source.PixelWidth;
                if (im.Width < width && im.Width != 0)
                    width = im.Width;

                im.Width = width;
                im.Height = source.PixelHeight * width / source.PixelWidth;
            }
        }

        void hyperlink_Click(object sender, RoutedEventArgs e)
        {

            if (HyperlinkClick != null)
            {
                HyperlinkClick(sender, new HyperlinkClickEventArgs((sender as Hyperlink).CommandParameter as Uri));
            }
        }

        /// <summary>
        /// Occurs when the left mouse button is clicked on a some Hyperlink in text.
        /// </summary>
        public event EventHandler<HyperlinkClickEventArgs> HyperlinkClick;

        /// <summary>
        /// Determine how 
        /// </summary>
        public HTMLTextBoxHyperlynkNavigaionPolitic NavigaionPolitic { get; set; }

        /// <summary>
        /// Occurs when the left mouse button is clicked on a some Image.
        /// </summary>
        public event EventHandler<ImageClickEventArgs> ImageClick;

        /// <summary>
        /// !!! Not Used
        /// </summary>
        public Brush HyperlinksForeground
        {
            get { return (Brush)GetValue(HyperlinksForegroundProperty); }
            set { SetValue(HyperlinksForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HyperlinksForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HyperlinksForegroundProperty =
            DependencyProperty.Register("HyperlinksForeground", typeof(Brush), typeof(HTMLViewer), new PropertyMetadata(new SolidColorBrush()));

        
    }
}
