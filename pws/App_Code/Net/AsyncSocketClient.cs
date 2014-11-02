using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Configuration;
using Seika.Transform.Command.Data;

namespace Seika.Net
{

    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public CommandData recvDat = new CommandData();
    }

    public class AsyncSocketClient
    {
        private static AutoResetEvent go =
            new AutoResetEvent(true);

        private static AutoResetEvent connectDone =
            new AutoResetEvent(false);
        private static AutoResetEvent sendDone =
            new AutoResetEvent(false);
        private static AutoResetEvent receiveDone =
            new AutoResetEvent(false);

        private static int BUFFER_SIZE = 10240;

        // The response from the remote device.
        //private static String response = String.Empty;
        static string Host = ConfigurationSettings.AppSettings["TransformServerHost"];
        static string Port = ConfigurationSettings.AppSettings["TransformServerPort"];

        public void SendData(CommandData sendData, ref CommandData resultData) 
        {
            go.WaitOne();
            Socket client = null;
            // Connect to a remote device.
            try
            {
                //connectDone.Reset();
                //sendDone.Reset();
                //receiveDone.Reset();

                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.Resolve(Host);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, int.Parse(Port));

                // Create a TCP/IP socket.
                client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                byte[] sendBuffer = new byte[BUFFER_SIZE];

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);

                connectDone.WaitOne();

                int length = 0;
                while ((length = sendData.GetBytes(sendBuffer)) > 0)
                {
                    Send(client, sendBuffer, 0, length);
                    sendDone.WaitOne();
                }

                // Receive the response from the remote device.

                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                Receive(state);
                receiveDone.WaitOne();

                resultData = state.recvDat;

                // Write the response to the console.
                //Console.WriteLine("Response received : {0}", response);

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                go.Set();
                if (client != null)
                {
                    try
                    {
                        // Release the socket.
                        client.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception) { }
                    try
                    {
                        client.Close();
                    }
                    catch (Exception) { }
                }

            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                // Signal that the connection has been made.
                connectDone.Set();
            }
        }

        private void Receive(StateObject state)
        {
            try
            {
                // Begin receiving the data from the remote device.
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {

                    // There might be more data, so store the data received so far.
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    state.recvDat.AddBytes(state.buffer, 0, bytesRead);

                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // Signal that all bytes have been received.
                    state.recvDat.MoveTop();
                    receiveDone.Set();
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
                receiveDone.Set();
            }
        }

        public void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            Send(client, byteData);
        }

        public void Send(Socket client, byte[] data)
        {
            Send(client, data, 0, data.Length);
        }

        public void Send(Socket client, byte[] byteData, int startIdx,  int length)
        {
            // Convert the string data to byte data using ASCII encoding.
            // Begin sending the data to the remote device.
            client.BeginSend(byteData, startIdx, length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                // Signal that all bytes have been sent.
                sendDone.Set();
            }
        }
    }
}