using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace Application4
{
    class ServerConnection
    {
        private static StreamSocket _socket = new StreamSocket();
        private  StreamSocketListener _listener = new StreamSocketListener();

        public static string IPaddress = "";
        private static bool connected = false;
        private static string CurrentUser = "";
        public ServerConnection(){}

        async public static void SendMessageToServer(StreamSocket socket, int msgType, string message)
        {
            var writer = new DataWriter(socket.OutputStream);
            writer.WriteString(msgType + message);
            var ret = await writer.StoreAsync();
            writer.DetachStream();
        }

        async public static void ConnectToServer(string ServerIP)
        {
            if (ServerIP != "")
            {
                IPaddress = ServerIP;
            }
            
            try
            {
                await _socket.ConnectAsync(new HostName(IPaddress), "3011");
                connected = true;
            }
            catch (Exception ex)
            {
                var md = new MessageDialog(ex.Message, "error");
                md.ShowAsync();
            }
        }

        public static bool isConnected()
        {
            return connected;
        }

        public static StreamSocket getStreamSocket()
        {
            return _socket;
        }

        public static string getCurrentUser()
        {
            return CurrentUser;
        }

        public static void setCurrentUser(string user)
        {
            CurrentUser = user;
        }

        async public static void WaitForData(StreamSocket socket)
        {
            var dr = new DataReader(socket.InputStream);
            int strLength = dr.ReadInt32();

            uint numStrBytes = await dr.LoadAsync((uint)strLength);
            string msg = dr.ReadString(numStrBytes);


            WaitForData(socket);
        }
    }


}
