using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Application4
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BasicPage1 : Application4.Common.LayoutAwarePage
    {
        public BasicPage1()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        async private void connectBtn_Click(object sender, RoutedEventArgs e)
        {
            ServerConnection.ConnectToServer(ServerIPBox.Text);
            LogMessage("Connected to "+ServerIPBox.Text, true);            
        }

        private void LogMessage(string message,Boolean green)
        {
            if (Dispatcher.HasThreadAccess)
                if (green)
                {
                    StatusBox.Foreground = new SolidColorBrush(Colors.Green);
                    StatusBox.Text = message + Environment.NewLine;
                }
                else
                {
                    StatusBox.Foreground = new SolidColorBrush(Colors.Red);
                    StatusBox.Text = message + Environment.NewLine;
                }
            else
            {
                Dispatcher.Invoke(Windows.UI.Core.CoreDispatcherPriority.Normal, (o, e) =>
                {
                    if (green)
                    {
                        StatusBox.Foreground = new SolidColorBrush(Colors.Green);
                        StatusBox.Text = message + Environment.NewLine;
                    }
                    else
                    {
                        StatusBox.Foreground = new SolidColorBrush(Colors.Red);
                        StatusBox.Text = message + Environment.NewLine;
                    }

                }, this, null);
            }
        }
    }
}
