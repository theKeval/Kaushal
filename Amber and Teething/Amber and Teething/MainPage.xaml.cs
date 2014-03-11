using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Amber_and_Teething.Resources;
using Amber_and_Teething.Helper;
using Microsoft.Phone.Tasks;

namespace Amber_and_Teething
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!(App.isAccept == "Accept"))
            {
                string disclaimer = "The information and claims provided in this application have not been evaluated by the Food and Drug Administration. All information is presented for general reference and education purposes, and is not intended to replace professional medical advice. We disclaim all liability in connection with use of this information.";
                //MessageBox.Show(disclaimer, "Amber & teething", MessageBoxButton.OK);

                CustomMessageBox messageBox = new CustomMessageBox()
                {
                    //Title = "Amber & Teething", //your title
                    Message = disclaimer, //your message
                    RightButtonContent = "Accept", // you can change this right and left button content
                    //LeftButtonContent = "No",
                };

                messageBox.Dismissed += (s2, e2) =>
                {
                    switch (e2.Result)
                    {
                        case CustomMessageBoxResult.RightButton:
                            //here your function for right button
                            App.isAccept = "Accept";
                            break;
                        //case CustomMessageBoxResult.LeftButton:
                        //    //here your function for left button
                        //    break;
                        default:
                            break;
                    }
                };

                messageBox.Height = Application.Current.Host.Content.ActualHeight;
                messageBox.Width = Application.Current.Host.Content.ActualWidth;
                messageBox.Padding = new Thickness(5);
                messageBox.Show();

            }
        }


        private void bdr_teethP1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/teeth_p1.xaml", UriKind.Relative), ((Border)sender).Name);
        }

        private void bdr_balticP1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/teeth_p1.xaml", UriKind.Relative), ((Border)sender).Name);
        }

        private void tblock_browse_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://www.amberteethingnecklace.org", UriKind.Absolute);
            wbt.Show();
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

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}