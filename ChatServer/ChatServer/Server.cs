using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    internal class Server
    {
        int port;
        Socket serverSocket;
        Dictionary<EndPoint, Socket> clientSockets;

        public Server(int port)
        {
            this.port = port;

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));

            clientSockets = new Dictionary<EndPoint, Socket>();
        }

        public void Start()
        {
            serverSocket.Listen(1000);

            Console.WriteLine("Listening on port {0}", port);

            Thread listenThread = new Thread(ListenWorker);
            listenThread.Start();

            Console.ReadKey();

            CloseSockets();
        }

        private void CloseSockets()
        {
            foreach(var socket in clientSockets.Values)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private void ListenWorker()
        {
            while(true)
            {
                try
                {
                    Socket socket = serverSocket.Accept();

                    lock (clientSockets)
                    {
                        clientSockets.Add(socket.RemoteEndPoint, socket);
                    }

                    ThreadPool.QueueUserWorkItem(state => ClientWorker(socket));

                    Console.WriteLine("{0} has connected", socket.RemoteEndPoint);
                }
                catch(SocketException)
                {
                    return; // when server socket gets closed in another thread?
                }
                catch (ObjectDisposedException)
                {
                    return; // server socket closed
                }
            }
        }

        private void ClientWorker(Socket socket)
        {
            while(true)
            {
                string data = null;

                // Read data from socket unit EOF
                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];

                        int bytesRec = socket.Receive(buffer);

                        if (bytesRec == 0) // client disconnected
                        {
                            // remove client from dictionary and close loop
                            lock (clientSockets)
                            {
                                clientSockets.Remove(socket.RemoteEndPoint);
                            }

                            Console.WriteLine("{0} has disconnected", socket.RemoteEndPoint);

                            socket.Shutdown(SocketShutdown.Both); // shutdown and close socket
                            socket.Close();

                            return;  // finish client worker upon disconnect
                        }

                        data += Encoding.ASCII.GetString(buffer, 0, bytesRec);

                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }
                    catch (SocketException)
                    {
                        return; // Socket was closed on a seperate thread before Recieve returned 0 on this thread, so close thread
                    }
                }

                Console.WriteLine("Message received: {0}", data);

                Broadcast(data);
            }
        }

        private void Broadcast(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);

            lock (clientSockets)
            {
                foreach (var socket in clientSockets.Values)
                {
                    try
                    {
                        int bytesSent = socket.Send(data);
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
    }
}
