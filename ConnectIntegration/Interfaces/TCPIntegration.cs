using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;

namespace AscomIntegration
{
    public class TCPIntegration : ConnectInterface
    {
        protected TcpClient _client;
        protected NetworkStream _stream;

        public TCPIntegration(string xml) : base(xml) { }

        //public TCPIntegration(string ipAddress, int port) : base(ipAddress, port) { }

        public override void Send(ConnectMessage message)
        {
            Connect();
            _numMessages++;
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message.ToString());

            try
            {
                // Send the message to the connected TcpServer. 
                _stream.Write(data, 0, data.Length);
                _numSends++;
            }
            catch (ArgumentNullException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString(), "ArgumentNullException");
                _numFails++;
            }
            catch (SocketException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString(), "SocketException");
                _numFails++;
            }

            Close();
        }

        protected override void Connect()
        {
            _client = new TcpClient();
            _client.Connect(_address, _port);
            _stream = _client.GetStream();
        }

        protected override void Close()
        {
            _stream.Close();
            _client.Close();
        }
    }
}