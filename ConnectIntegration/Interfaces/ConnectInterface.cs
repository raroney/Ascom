using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Xml.Linq;
using System.Diagnostics;


namespace AscomIntegration
{
    public enum DebugLevel { High, Normal, None };

    public abstract class ConnectInterface : ConnectObject
    {
        private const int NUM_ATTEMPTS = 36;

        protected string _address;
        protected int _port;
        protected DebugLevel _debugLevel;
        protected int _numMessages;
        protected int _numSends;
        protected int _numFails;

        protected Queue<ConnectMessage> _messages = new Queue<ConnectMessage>();

        protected ConnectInterface(string xml)
        {
            LoadXml(xml);
        }

        private void LoadXml(string xml)
        {
            XElement element = XElement.Parse(xml);
            _address = element.Element("Address").Value;
            _port = Int32.Parse(element.Element("Port").Value);
            ResetStatistics();
        }

        public abstract void Send(ConnectMessage message);
        protected abstract void Connect();
        protected abstract void Close();

        public void SetDebugLevel(DebugLevel lvl)
        {
            _debugLevel = lvl;
        }

        public void ResetStatistics()
        {
            _numMessages = 0;
            _numSends = 0;
            _numFails = 0;
        }

        public void ReportStatistics()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().ToString());
            str.Append(" Messages to: ");
            str.Append(_address);
            str.Append(":");
            str.Append(_port);
            str.Append(" Num messages: ");
            str.Append(_numMessages);
            str.Append(" Sends OK: ");
            str.Append(_numSends);
            str.Append(" Errors: ");
            str.Append(_numFails);
            Debug.WriteLine(str.ToString());
            ResetStatistics();
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public Queue<ConnectMessage> Messages
        {
            get { return _messages; }
        }

        public void EnqueueMessage(ConnectMessage msg)
        {
            _messages.Enqueue(msg);
        }

        public void SendAllMessages()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().ToString());
            str.Append(" Started at: ");
            str.Append(DateTime.Now.ToString());
            Debug.WriteLine(str.ToString());

            int number = 0;
            int attempts = 0;

            for (int repeat = 0; repeat < Repeat; repeat++)
            {
                foreach (ConnectMessage message in _messages)
                {
                    for (int msgRepeat = 0; msgRepeat < message.Repeat; msgRepeat++)
                    {
                        try
                        {
                            message.Index = ++number;
                            Debug.WriteLineIf(_debugLevel == DebugLevel.High, DateTime.Now.ToString("HH:mm:ss") + " - [" + this.GetType().ToString() + "] Sending message: " + message.ToString());
                            Send(message);
                            Thread.Sleep(message.Delay);
                        }
                        catch (Exception ex)
                        {
                            // log and wait 5secs to retry
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Message Send Failed (");
                            sb.Append(DateTime.Now.ToString("HH:mm:ss"));
                            sb.Append(": ");
                            sb.Append(ex.Message);

                            Debug.WriteLine(sb.ToString());
                            Thread.Sleep(5000);

                            if (attempts < NUM_ATTEMPTS)
                            {
                                // roll back increments
                                msgRepeat--;
                                number--;
                            }
                            else
                            {
                                // log the error and throw an exception
                                sb = new StringBuilder();
                                sb.Append(NUM_ATTEMPTS);
                                sb.Append(" attempts to resend failed.");
                                Debug.WriteLine(sb.ToString());

                                throw new Exception(sb.ToString(), ex);
                            }
                        }
                    }
                }

                Thread.Sleep(Delay);
            }

            str.Clear();
            str.Append(this.GetType().ToString());
            str.Append(" Ended at: ");
            str.Append(DateTime.Now.ToString());
            Debug.WriteLine(str.ToString());
            ReportStatistics();
        }
    }
}
