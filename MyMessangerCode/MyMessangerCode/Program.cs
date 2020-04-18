using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyMessangerCode
{
    class MainClass
    {

        public static Socket socket;
        public static EndPoint epLocal, epRemote;


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//no connection requered

            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress,true);

            Console.WriteLine("Yore IP is {0}", GetLocalIP());
            do
            {
                StarTConnection();

                SendMessage();
            } while (true);
        }
        private static string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
                
            }
                    return null;

        }
        private static void MassageCallBack(IAsyncResult result)
        {
            try
            {
                int size = socket.EndReceiveFrom(result, ref epRemote);
                if (size > 0)
                {
                    byte[] recievedData = new byte[1464];
                    recievedData = (byte[])result.AsyncState;

                    ASCIIEncoding encoding = new ASCIIEncoding();
                    String recivedMessage = encoding.GetString(recievedData);

                    Console.WriteLine("Friend: "+recivedMessage);

                    byte[] buffer = new byte[1464];

                    socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MassageCallBack), buffer);


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void StarTConnection()
        {
            try
            {
                Console.WriteLine("using port 80");

                epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 100);
                socket.Bind(epLocal);

                String friendIp;//put friend ip
                epRemote = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 101);
                socket.Connect(epRemote);

                byte[] buffer = new byte[1464];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MassageCallBack), buffer);

                Console.WriteLine("Connected");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void SendMessage()
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] message = new byte[1464];

                string mymassage=Console.ReadLine();
                message= encoding.GetBytes(mymassage);

                socket.Send(message);
                Console.WriteLine("You: ",mymassage);


            }
            catch (Exception ex)
            {

            }
        }
    }
}
