using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using System.IO;
using System.IO.IsolatedStorage;
using Amber_and_Teething.Helper;
using Microsoft.Phone.Tasks;
using System.Text;

namespace Amber_and_Teething
{
    public partial class teeth_p1 : PhoneApplicationPage
    {
        public teeth_p1()
        {
            InitializeComponent();
        }

        //string abc;
        public StorageFile storageFolder_teethPages;
        public static string navigatedData;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                navigatedData = (string)NavigationService.GetNavigationData();

                Uri uri;
                switch (navigatedData)
                {
                    case "bdr_teethP1":

                        uri = new Uri("/HtmlContent/teeth_p1.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_teethP2":

                        uri = new Uri("/HtmlContent/teeth_p2.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_teethP3":

                        uri = new Uri("/HtmlContent/teeth_p3.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_teethP4":

                        uri = new Uri("/HtmlContent/teeth_p4.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_teethP5":

                        uri = new Uri("/HtmlContent/teeth_p5.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;



                    case "bdr_balticP1":

                        uri = new Uri("/HtmlContent/baltic_p1.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_balticP2":

                        uri = new Uri("/HtmlContent/baltic_p2.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_balticP3":

                        uri = new Uri("/HtmlContent/baltic_p3.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_balticP4":

                        uri = new Uri("/HtmlContent/baltic_p4.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    case "bdr_balticP5":

                        uri = new Uri("/HtmlContent/baltic_p5.html", UriKind.Relative);
                        wb_teethP1.Navigate(uri);

                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            #region
            ////storageFolder_teethPages = await Windows.Storage.StorageFolder.GetFolderFromPathAsync("ms-appx:///TeethPages/");
            ////storageFolder_teethPages = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///TeethPages/teeth_p1.html"));

            ////using (var abc = await storageFolder_teethPages.OpenReadAsync())
            ////{

            ////}

            //string text;
            //var src = Application.GetResourceStream(new Uri(@"/teeth_p1.html", UriKind.Relative)).Stream;
            //using (StreamReader sr = new StreamReader(src))
            //{
            //    text = sr.ReadToEnd();
            //}
            #endregion

            #region
            //StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///teeth_p1.html"));

            //// Read the data.
            //using (StreamReader streamReader = new StreamReader(await file.OpenStreamForReadAsync()))
            //{
            //    string s = streamReader.ReadToEnd();

            //    MessageBox.Show(s);
            //}
            #endregion


            #region HtmlViewer working code - commented
            //StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            //// Get fileStream
            //Stream fileStream = await folder.OpenStreamForReadAsync("teeth_p1.html");

            //// Read the data.
            //using (StreamReader streamReader = new StreamReader(fileStream))
            //{
            //    string s = streamReader.ReadToEnd();

            //    ie_teethP1.Html = "<p>" + s + "</p>";

            //    //MessageBox.Show(s);
            //}
            #endregion

        }

        private void tblock_share_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask()
            {
                Title = "Amber & Teething",
                LinkUri = new Uri("http://www.amberteethingnecklace.org/", UriKind.Absolute),

            };
            shareLinkTask.Show();
        }

        private void tblock_browse_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            switch (navigatedData)
            {
                case "bdr_teethP1":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/how-do-amber-teething-necklaces-work/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_teethP2":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/top-tips-when-shopping-for-amber-teething-jewelry/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_teethP3":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/easy-tests-to-identify-real-amber/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_teethP4":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/common-misconceptions-about-ambe-teething-necklaces/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_teethP5":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/advices-for-using-amber-teething-jewelry/", UriKind.Absolute);
                    wbt.Show();

                    break;



                case "bdr_balticP1":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/amber-in-medicine/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_balticP2":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/relatives-of-baltic-amber/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_balticP3":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/composition-morphology-baltic-amber/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_balticP4":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/the-amber-room/", UriKind.Absolute);
                    wbt.Show();

                    break;

                case "bdr_balticP5":

                    wbt.Uri = new Uri("http://www.amberteethingnecklace.org/myths-legends-baltic-amber/", UriKind.Absolute);
                    wbt.Show();

                    break;

                default:
                    break;
            }
        }


        //private void ApplyStyleSheet(string cssFilename)
        //{
        //    try
        //    {
        //        var sr = Application.GetResourceStream(new Uri(cssFilename, UriKind.Relative));
        //        using (var strm = sr.Stream)
        //        using (var reader = new StreamReader(strm))
        //        {
        //            string css = reader.ReadToEnd().Replace("\r", "").Replace("\n", "");

        //            var scriptToRun =
        //                "(function() {" +
        //                "  var pa= document.getElementsByTagName('head')[0] ; " +
        //                "  var el= document.createElement('style'); " +
        //                "  el.type= 'text/css'; " +
        //                "  el.media= 'screen'; " +
        //                "  el.styleSheet.cssText= '" + css + "'; " +
        //                "  pa.appendChild(el); " +
        //                "})();";

        //            //wb_teethP1.InvokeScript(scriptToRun);

        //            //wb_teethP1.InvokeScript("eval", new string[] { });
        //            wb_teethP1.InvokeScript("eval", scriptToRun);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void wb_teethP1_Loaded(object sender, RoutedEventArgs e)
        {
            //ApplyStyleSheet("style.css");
        }

        private void wb_teethP1_Navigated(object sender, NavigationEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("function setCSS() { ");
                sb.Append("var styleElement = document.createElement('style'); ");
                sb.Append("var styleText = 'h1 { font-family: cambria; font-size: 23px; font-weight: bold; } .widget { width:100%; } .content p strong { color: #C76328; font-size: 18px; font-weight: bold; } li { color: #5D5D5D; font-size: 15px; } .content p { color: #5D5D5D; font-size: 15px; } .article img { width: 100%; }'; ");
                sb.Append("var headElements = document.getElementsByTagName('head'); ");
                sb.Append("styleElement.type = 'text/css'; ");
                sb.Append("if (headElements.length == 1) { ");
                sb.Append("   headElements[0].appendChild(styleElement); ");
                sb.Append("} else if (document.head) { ");
                sb.Append("document.head.appendChild(styleElement); ");
                sb.Append("} ");
                sb.Append("if (styleElement.styleSheet) { ");
                sb.Append("styleElement.styleSheet.cssText = styleText; ");
                sb.Append("} ");
                sb.Append("} ");

                string script = sb.ToString();
                wb_teethP1.IsScriptEnabled = true;
                wb_teethP1.InvokeScript("eval", new string[] { script });
                wb_teethP1.InvokeScript("eval", new string[] { "setCSS()" });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //void addCss()
        //{
            
        //}

    }
}