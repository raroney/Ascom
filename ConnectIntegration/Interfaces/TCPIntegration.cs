﻿using System;
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
            //Debug.WriteLineIf(_debugLevel == DebugLevel.High, DateTime.Now.ToString("HH:mm:ss") + " - Sending TCP message with body: " + message.Body);
            _numMessages++;
            SendData(message.ToString());
            Close();
        }

        protected override void Connect()
        {
            _client = new TcpClient();
            _client.Connect(_address, _port);
            _stream = _client.GetStream();
        }

        protected override void SendData(string msg)
        {
            try
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);

                // Send the message to the connected TcpServer. 
                _stream.Write(data, 0, data.Length);
                _numSends++;

                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                //data = new Byte[256];

                // Read the first batch of the TcpServer response bytes.
                //Int32 bytes = _stream.Read(data, 0, data.Length);
                //string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
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
        }

        protected override void Close()
        {
            _stream.Close();
            _client.Close();
        }
    }
}