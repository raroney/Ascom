using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AscomIntegration
{
    public class ConnectMessage : ConnectObject
    {
        private const string NUMBER_PLACEHOLDER = "||number||";
        private const string TIME_PLACEHOLDER = "||time||";

        protected string _body;
        private int _index = 0;

        public ConnectMessage(string xml)
        {
            XElement element = XElement.Parse(xml);
            _body = element.Element("Body").Value;
        }

        public string Body
        {
            get { return _body; }
        }

        internal int Index
        {
            set { _index = value; }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder(_body);
            str.Replace(NUMBER_PLACEHOLDER, _index.ToString());
            str.Replace(TIME_PLACEHOLDER, DateTime.Now.ToString("HH:mm:ss"));

            return str.ToString();
        }
    }
}
