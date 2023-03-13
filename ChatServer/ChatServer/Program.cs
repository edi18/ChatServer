

namespace ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(27015);
            server.Start();
        }
    }

    /*internal class Program
    {
        static Dictionary<EndPoint, Socket> clients;
        static Socket listener;
        static Thread listenThread;

        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 27015);

            //Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);

            listener.Listen(1000);

            Console.WriteLine("Listening on port 27015");

            clients = new Dictionary<EndPoint, Socket>(); // initialize client dictionary

            listenThread = new Thread(ServerListen);
            listenThread.Start();

            Console.ReadKey();
            
            listener.Close();
        }

        private static void ServerListen()
        {
            while (true)
            {
                try
                {
                    // wait for new connection to accept
                    Socket clientSocket = listener.Accept(); // blocking accept
                    // add new client to client dictionary
                    lock (clients)
                    {
                        clients.Add(clientSocket.RemoteEndPoint, clientSocket);
                    }
                    ThreadPool.QueueUserWorkItem(state => HandleClient(clientSocket));
                    Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + " has connected.");
                }
                catch (SocketException e) 
                {
                    break;
                }
            }
        }

        private static void HandleClient(Socket clientSocket)
        {
            while (true)
            {
                try
                {
                    string data = null;

                    // Read data from socket unit EOF
                    while (true)
                    {
                        byte[] buffer = new byte[1024];

                        int bytesRec = clientSocket.Receive(buffer);

                        if (bytesRec == 0) // client disconnected
                        {
                            // remove client from dictionary and close loop
                            lock (clients)
                            {
                                clients.Remove(clientSocket.RemoteEndPoint);
                            }
                            Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + " has disconnected.");
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                            return;
                        }

                        data += Encoding.ASCII.GetString(buffer, 0, bytesRec);

                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("Message received: {0}", data);

                    BroadcastMessageASCII(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //break;
                }
            }
        }

        private static void BroadcastMessageASCII(string msg)
        {
            byte[] data = Encoding.ASCII.GetBytes(msg);

            lock (clients)
            {
                foreach (var client in clients.Values)
                {
                    try
                    {
                        int bytesSent = client.Send(data);
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unexpected exception : {0}", ex.ToString());
                    }
                }
            }
        }
    }*/
}
