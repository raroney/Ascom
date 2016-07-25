using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;

namespace AscomIntegration
{
    public class TAPInterface : TCPIntegration
    {
        // sample message "<STX>Address<CR>Sample Message<CR><ETX><CHKSUM><CR>"
        private const char STX = (char)2;   // Start of Text
        private const char ETX = (char)3;   // End of Text
        private const char EOT = (char)4;   // End of Transmission
        private const char CR = (char)13;   // Carriage Return
        private const char ESC = (char)27;  // Escape

        private string _pagerID;
        private string _endTrans;

        public TAPInterface(string xml) : base(xml)
        {
            SetupMessages();
        }

        //public TAPInterface(string ip, int port) : base(ip, port)
        //{
        //    SetupMessages();
        //}

        private void SetupMessages()
        {
            StringBuilder PagerID = new StringBuilder(ESC.ToString());
            PagerID.Append("PG1");
            PagerID.Append(CR.ToString());
            _pagerID = PagerID.ToString();

            StringBuilder endTrans = new StringBuilder(EOT.ToString());
            endTrans.Append(CR.ToString());
            _endTrans = endTrans.ToString();
        }

        public override void Send(ConnectMessage message)
        {
            Connect();
            //Debug.WriteLineIf(_debugLevel == DebugLevel.High, DateTime.Now.ToString("HH:mm:ss") + " - Sending TAP message with body: " + message.Body);
            _numMessages++;
            SendData(CR.ToString());
            SendData(_pagerID);
            SendData(message.ToString());
            SendData(_endTrans);
            Close();
        }

        private void SendData(string msg)
        {
            try
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);

                // Send the message to the connected TcpServer. 
                _stream.Write(data, 0, data.Length);
                
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                data = new Byte[256];

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = _stream.Read(data, 0, data.Length);
                string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                //TODO: interpret response data to check if it really worked.
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
        }

        internal static string TAPString(string address, string message)
        {
            StringBuilder str = new StringBuilder();
            str.Append(STX);
            str.Append(address);
            str.Append(CR);
            str.Append(message);
            str.Append(CR);
            str.Append(ETX);
            str.Append(CheckSum(str.ToString()));
            str.Append(CR);

            return str.ToString();
        }

        private static string CheckSum(string msg)
        {
            int sum = 0;

            foreach (char ch in msg)
            {
                sum += (int)ch;
            }

            string bits = Convert.ToString(sum, 2).PadLeft(12, '0');
            if (bits.Length > 12) bits = bits.Substring(bits.Length - 12);

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < 12; i += 4)
            {
                str.Append((char)(Convert.ToInt32(bits.Substring(i, 4), 2) + 48));
            }

            return str.ToString();
        }
    }
}