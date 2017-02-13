using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Networking.Sockets;
using System.Net;
using System.IO;
using System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Application4
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage : Page
    {

        private DispatcherTimer timer = new DispatcherTimer();

        public BlankPage()
        {
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            this.InitializeComponent();
            timer.Tick += updateUserName;
            timer.Interval = new TimeSpan(00,00,3);
            timer.Start();
        }



        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage1), e.OriginalSource);      
            showPage2(true);
            showPage3(true);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage2), e.OriginalSource);
            showPage1(false);
            showPage3(false);
            showPage4(true);
            showPage5(true);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage3), e.OriginalSource);
            showPage1(false);
            showPage2(false);
            showPage4(true);
            showPage5(true);

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage4), e.OriginalSource);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage5), e.OriginalSource);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage6), e.OriginalSource);
        }

        private void showPage1(bool show)
        {
            if (show)
            {
                Page1.Opacity = 100;
            }
            else
            {
                Page1.Opacity = 20;
            }
        }

        private void showPage2(bool show)
        {
            if (show)
            {
                Page2.Opacity = 100;
            }
            else
            {
                Page2.Opacity = 20;
            }
        }

        private void showPage3(bool show)
        {
            if (show)
            {
                Page3.Opacity = 100;
            }
            else
            {
                Page3.Opacity = 20;
            }
        }

        private void showPage4(bool show)
        {
            if (show)
            {
                Page4.Opacity = 100;
            }
            else
            {
                Page4.Opacity = 20;
            }
        }

        private void showPage5(bool show)
        {
            if (show)
            {
                Page5.Opacity = 100;
            }
            else
            {
                Page5.Opacity = 20;
            }
        }

        private void showPage6(bool show)
        {
            if (show)
            {
                Page6.Opacity = 100;
            }
            else
            {
                Page6.Opacity = 20;
            }
        }

        private void updateUserName(object sender, object e)
        {
            if (ServerConnection.getCurrentUser() != "")
            {
                userName.Text = ServerConnection.getCurrentUser();
            }
        }
    }
}
