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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string navigatedData = (string)NavigationService.GetNavigationData();

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

    }
}