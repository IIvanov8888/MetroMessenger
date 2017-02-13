using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI;
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
    public sealed partial class BasicPage2 : Application4.Common.LayoutAwarePage
    {
        public BasicPage2()
        {
            this.InitializeComponent();
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
            if (RegUsernameBox.Text == "")
            {
                StatusBox.Text = "Please fill the username box!";
            }
            else if (RegPasswordBox_1.Password != RegPasswordBox_2.Password)
            {
                StatusBox.Text = "The password must match!";
            }
            else
            {
                ServerConnection.SendMessageToServer(ServerConnection.getStreamSocket(), 1, RegUsernameBox.Text + "\r\n");
                ServerConnection.SendMessageToServer(ServerConnection.getStreamSocket(), 1, RegPasswordBox_1.Password + "\r\n");
                ServerConnection.setCurrentUser(RegUsernameBox.Text);

                StatusBox.Foreground = new SolidColorBrush(Colors.Green);
                StatusBox.Text = "You created new account and you are logged in now! You are able to chat with the other users!";
            }

        }
    }
}
