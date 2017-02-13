using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace MetroMessengerServer
{
    class MessengerServer
    {

        // This hash table stores users and connections (browsable by user)
        public static Hashtable htUsers = new Hashtable(30); // 30 users at one time limit
        // This hash table stores connections and users (browsable by connection)
        public static Hashtable htConnections = new Hashtable(30); // 30 users at one time limit
        // Will store the IP address passed to it
        private IPAddress ipAddress;
        private TcpClient tcpClient;
        // The event and its argument will notify the form when a user has connected, disconnected, send message, etc.
        public static event StatusChangedEventHandler StatusChanged;
        public static StatusChangedEventArgs e;
        private static DBConnection dbconn = new DBConnection("localhost", "metromessenger", "root", "");
        // The constructor sets the IP address to the one retrieved by the instantiating object
        public MessengerServer(IPAddress address)
        {
            ipAddress = address;
        }

        // The thread that will hold the connection listener
        private Thread thrListener;

        // The TCP object that listens for connections
        private TcpListener tlsClient;

        // Will tell the while loop to keep monitoring for connections
        bool ServRunning = false;

        public static DBConnection getDBC()
        {
            return dbconn;
        }

        // Add the user to the hash tables
        public static void AddUser(TcpClient tcpUser, string strUsername)
        {
            // First add the username and associated connection to both hash tables
            MessengerServer.htUsers.Add(strUsername, tcpUser);
            MessengerServer.htConnections.Add(tcpUser, strUsername);
        }

        // Remove the user from the hash tables
        public static void RemoveUser(TcpClient tcpUser)
        {
            // If the user is there
            if (htConnections[tcpUser] != null)
            {
                // Remove the user from the hash table
                MessengerServer.htUsers.Remove(MessengerServer.htConnections[tcpUser]);
                MessengerServer.htConnections.Remove(tcpUser);
            }
        }

        // This is called when we want to raise the StatusChanged event
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                // Invoke the delegate
                statusHandler(null, e);
            }
        }

        public static void getOnlineUsers(string To)
        {
            StreamWriter swSenderSender;
            string Message = "4";

            // Create an array of TCP clients and usernames, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[MessengerServer.htUsers.Count];
            String[] usernames = new String[MessengerServer.htUsers.Count];

            // Copy the TcpClient objects and the username strings into the arrays
            MessengerServer.htUsers.Values.CopyTo(tcpClients, 0);
            MessengerServer.htUsers.Keys.CopyTo(usernames, 0);

            foreach (string user in usernames)
            {
                Message += "," + user;
            }

             // Loop through the list of TCP clients
            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (usernames[i] == To)
                    {

                        // Send the message to the current user in the loop
                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                        swSenderSender.WriteLine(Message);

                        swSenderSender.Flush();
                        swSenderSender = null;

                    }
                }
                catch // If there was a problem, the user is not there anymore, remove him
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        // Send messages from one user to another user the others
        public static void SendMessage(string From,string To, string Message)
         {
             StreamWriter swSenderSender;

             // Create an array of TCP clients and usernames, the size of the number of users we have
             TcpClient[] tcpClients = new TcpClient[MessengerServer.htUsers.Count];
             String[] usernames = new String[MessengerServer.htUsers.Count];

             // Copy the TcpClient objects and the username strings into the arrays
             MessengerServer.htUsers.Values.CopyTo(tcpClients, 0);
             MessengerServer.htUsers.Keys.CopyTo(usernames, 0);

             // Loop through the list of TCP clients
             for (int i = 0; i < tcpClients.Length; i++)
             {
                 try
                 {
                     // If the message is blank or the connection is null, break out
                     if (Message.Trim() == "" || tcpClients[i] == null)
                     {
                         continue;
                     }

                     if (usernames[i] == To)
                     {

                     // Send the message to the current user in the loop
                     Message = From + " says: " + Message;

                     swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                     swSenderSender.WriteLine(Message);

                     swSenderSender.Flush();
                     swSenderSender = null;
                     
                     }
                 }
                 catch // If there was a problem, the user is not there anymore, remove him
                 {
                     RemoveUser(tcpClients[i]);
                 }
             }
         }

        public void StartListening()
        {

            // Get the IP of the first network device, however this can prove unreliable on certain configurations
            IPAddress ipaLocal = ipAddress;

            // Create the TCP listener object using the IP of the server and the specified port
            tlsClient = new TcpListener(ipaLocal, 3011);

            // Start the TCP listener and listen for connections
            tlsClient.Start();

            // The while loop will check for true in this before checking for connections
            ServRunning = true;

            // Start the new tread that hosts the listener
            thrListener = new Thread(KeepListening);
            thrListener.Start();
        }

        private void KeepListening()
        {
            // While the server is running
            while (ServRunning == true)
            {
                // Accept a pending connection
                tcpClient = tlsClient.AcceptTcpClient();
                // Create a new instance of Connection
                Connection newConnection = new Connection(tcpClient);
            }
        }
    }
    // Holds the arguments for the StatusChanged event
    public class StatusChangedEventArgs : EventArgs
    {
        // The argument we're interested in is a message describing the event
        private string EventMsg;

        // Property for retrieving and setting the event message
        public string EventMessage
        {
            get
            {
                return EventMsg;
            }
            set
            {
                EventMsg = value;
            }
        }

        // Constructor for setting the event message
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    // This delegate is needed to specify the parameters we're passing with our event
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    // This class handels connections; there will be as many instances of it as there will be connected users
    class Connection
    {
        TcpClient tcpClient;
        // The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string currPass;
        private string strResponse;
        private string currUserMessage;
        private string currPassMessage;
        private string strTo;

        // The constructor of the class takes in a TCP connection
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            // The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            // The thread calls the AcceptClient() method
            thrSender.Start();
        }

        private void CloseConnection()
        {
            // Close the currently open objects
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        // Occures when a new client is accepted
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());
           
            MessengerServer.e = new StatusChangedEventArgs("A user has connected to the server.");
            MessengerServer.OnStatusChanged(MessengerServer.e);

            currUser = "Unknown";

            //receiving the username and password from the client
            currUserMessage = srReceiver.ReadLine();
            currPassMessage = srReceiver.ReadLine();

            //Check if the user wants to sign up or sign in | 1 is for sign up, 2 is for sign in| anything else will terminate the connection
            if (Convert.ToInt32(currUserMessage.Substring(0, 1)) == 1)
            {

                currUser = currUserMessage.Substring(1, currUserMessage.Length - 1);
                currPass = currPassMessage.Substring(1, currPassMessage.Length - 1);

                MessengerServer.getDBC().addUser(currUser, currPass);

                // Add the user to the hash tables and start listening for messages from him
                MessengerServer.AddUser(tcpClient, currUser);

                //Log the action in the server's log
                MessengerServer.e = new StatusChangedEventArgs("A new account was added to the database: " + currUser);
                MessengerServer.OnStatusChanged(MessengerServer.e);

                MessengerServer.e = new StatusChangedEventArgs(currUser + " logged in the system!");
                MessengerServer.OnStatusChanged(MessengerServer.e);

            }
            else if (Convert.ToInt32(currUserMessage.Substring(0, 1)) == 2)
            {
                //Getting the username and password
                currUser = currUserMessage.Substring(1, currUserMessage.Length - 1);
                currPass = currPassMessage.Substring(1, currPassMessage.Length - 1);

                //Check if the user is online we can not login again
                if (MessengerServer.htUsers.Contains(currUser) == false)
                {
                    // Add the user to the hash tables and start listening for messages from him
                    MessengerServer.AddUser(tcpClient, currUser);

                    //Log the action in the server's log
                    MessengerServer.e = new StatusChangedEventArgs(currUser + " logged in the system!");
                    MessengerServer.OnStatusChanged(MessengerServer.e);
                }
            }
            else
            {
                CloseConnection();
                return;
            }

            try
            {
                strResponse = "0";
                int msgType = 0;

                // Keep waiting for a message from the user
                while (strResponse != "")
                {
                    //the username of the user to which the message will be send
                    strTo = srReceiver.ReadLine();
                    strTo = strTo.Substring(1, strTo.Length - 1);

                    //The message type, followed by the actual message 
                    strResponse = srReceiver.ReadLine();
                    
                    msgType = Convert.ToInt32(strResponse.Substring(0, 1));
                    strResponse = strResponse.Substring(1, strResponse.Length - 1);
                    
                    //if the messege is req for online users
                    if (msgType == 4)
                    {
                        MessengerServer.getOnlineUsers(strTo);
                    }
                    //if the message is not for sign up(code 1) and sign ip(code 2)
                    else if (msgType != 1 && msgType != 2)
                    {

                        MessengerServer.SendMessage(currUser, strTo, strResponse);

                        MessengerServer.e = new StatusChangedEventArgs(currUser + " says to " + strTo + ": " + strResponse);
                        MessengerServer.OnStatusChanged(MessengerServer.e);
                    }
                }
            }
            catch
            {
                // If anything went wrong with this user, disconnect him
                MessengerServer.RemoveUser(tcpClient);
                CloseConnection();
            }
        }
    }

}


