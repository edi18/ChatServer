using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClient
{
    internal class Client
    {
        string host;
        int port;
        Socket socket;
        Action<string> receiveCallback;

        public Client(string host, int port, Action<string> receiveCallback)
        {
            this.host = host;
            this.port = port;
            this.receiveCallback = receiveCallback;

            // Create TCP/IPv4 socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            socket.Connect(host, port); // needs an exception on connect

            Thread receiveThread = new Thread(RecieveWorker);
            receiveThread.Start();
        }

        public void Close()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private void RecieveWorker()
        {
            while (true)
            {
                string data = null;

                // Read data from socket unit EOF
                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];

                        int bytesRec = socket.Receive(buffer);

                        data += Encoding.ASCII.GetString(buffer, 0, bytesRec);

                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }
                    catch(SocketException)
                    {
                        return; // socket was closed (raises blocking exception), then close thread
                    }
                }

                data = data.Replace("<EOF>", ""); // remove end of message delimiter

                receiveCallback(data);
            }
        }

        public void SendMessage(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(socket.LocalEndPoint + ": " + message + "<EOF>");

            try
            {
                int bytesSent = socket.Send(bytes);
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
