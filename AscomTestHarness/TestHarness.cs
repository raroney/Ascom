using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using AscomIntegration;
using System.ComponentModel;

namespace AscomTestHarness
{
    public enum DebugLevel { High, Normal, None };
    
    public class TestHarness : INotifyPropertyChanged
    {
        private static readonly TestHarness _instance = new TestHarness();
        private Queue<ConnectInterface> _integrations;
        private DebugLevel _debugLevel;
        //private static bool _running = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;

        static TestHarness() { }

        private TestHarness() { }

        public static TestHarness Instance
        {
            get { return _instance; }
        }

        //public bool Running
        //{
        //    get { return _running; }

        //    private set
        //    {
        //        if (_running != value)
        //        {
        //            _running = value;
        //            OnPropertyChanged("Running");
        //        }
        //    }
        //}

        public object LogLevel { get; set; }

        public void SetupLog(DebugLevel lvl, string logFile)
        {
            _debugLevel = lvl;
            TextWriterTraceListener listener = new TextWriterTraceListener(logFile);
            listener.TraceOutputOptions |= TraceOptions.DateTime;
            Debug.Listeners.Add(listener);
            Debug.AutoFlush = true;
        }

        public void LoadXml(string fileName)
        {
            _integrations = new Queue<ConnectInterface>();

            XDocument doc = XDocument.Load(XmlReader.Create(fileName));
            XElement root = doc.Element("Test");
            _name = root.Attribute("name").Value;

            // build object for connections and messages from XML definition
            foreach (XElement element in root.Elements("Integration"))
            {
                if (Convert.ToBoolean(element.Attribute("active").Value))
                {
                    // get connection details
                    XElement conn = element.Element("Connection");

                    // create connect object
                    StringBuilder interfaceType = new StringBuilder("AscomIntegration.");
                    interfaceType.Append(element.Attribute("type").Value);
                    interfaceType.Append(", AscomIntegration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                    ConnectInterface integration = (ConnectInterface)Activator.CreateInstance(Type.GetType(interfaceType.ToString()), conn.ToString());

                    // add to collection                
                    _integrations.Enqueue(integration);
                    Debug.WriteLineIf(_debugLevel > DebugLevel.None, integration.ToString(), "Loaded");

                    // get messages
                    foreach (XElement messages in element.Elements("Messages"))
                    {
                        if (Convert.ToBoolean(messages.Attribute("active").Value))
                        {
                            StringBuilder messageType = new StringBuilder("AscomIntegration.");
                            messageType.Append(messages.Attribute("type").Value);
                            messageType.Append(", AscomIntegration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                            Type msgType = Type.GetType(messageType.ToString());
                            integration.Name = messages.Attribute("name").Value;
                            integration.Delay = Int32.Parse(messages.Attribute("delay").Value);
                            integration.Repeat = Int32.Parse(messages.Attribute("repeat").Value);

                            Debug.Indent();
                            // Add each message to this integration
                            foreach (XElement message in messages.Elements("Message"))
                            {
                                if (Convert.ToBoolean(message.Attribute("active").Value))
                                {
                                    // create message object
                                    ConnectMessage msg = (ConnectMessage)Activator.CreateInstance(msgType, message.ToString());
                                    // set message properties
                                    msg.Name = messages.Attribute("name").Value;
                                    msg.Delay = Convert.ToInt32(message.Attribute("delay").Value);
                                    msg.Repeat = Convert.ToInt32(message.Attribute("repeat").Value);
                                    msg.ToString();
                                    integration.EnqueueMessage(msg);

                                    Debug.WriteLineIf(_debugLevel == DebugLevel.High, msg.ToString(), "Loaded");
                                }
                            }
                        }

                        Debug.Unindent();
                    }
                }
            }
        }

        public void RunScript()
        {
            StringBuilder str = new StringBuilder();
            str.Append(_name);
            str.Append(" Started at: ");
            str.Append(DateTime.Now.ToString());
            Debug.WriteLine(str.ToString());

            //Running = true;
            Parallel.ForEach(_integrations, integration => integration.SendAllMessages());
            //Running = false;

            str.Clear();
            str.Append(_name);
            str.Append(" Ended at: ");
            str.Append(DateTime.Now.ToString());
            Debug.WriteLine(str.ToString());
            Debug.Flush();
        }


        //public void RunScript(object sender, DoWorkEventArgs e)
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append(_name);
        //    str.Append(" Started at: ");
        //    str.Append(DateTime.Now.ToString());
        //    Debug.WriteLine(str.ToString());

        //    //Running = true;
        //    Parallel.ForEach(_integrations, integration => integration.SendAllMessages());
        //    //Running = false;

        //    str.Clear();
        //    str.Append(_name);
        //    str.Append(" Ended at: ");
        //    str.Append(DateTime.Now.ToString());
        //    Debug.WriteLine(str.ToString());
        //    Debug.Flush();
        //}

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
