using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Diagnostics;
using System.ComponentModel;

namespace AscomIntegration
{
    public class SMTPInterface : ConnectInterface//, IDisposable
    {
        //private SmtpClient _client;
        //private MailMessage _email;

        public SMTPInterface(string xml) : base(xml)
        {
            Connect();
        }

        //public SMTPInterface(string ipAddress, int port) : base(ipAddress, port) { }

        public override void Send(ConnectMessage message)
        {
            MailAddress from = new MailAddress(((SMTPMessage)message).To);
            MailAddress to = new MailAddress(((SMTPMessage)message).From);

            using (MailMessage email = new MailMessage(from, to))
            {
                email.Body = message.ToString();
                email.Subject = ((SMTPMessage)message).Subject;
                _numMessages++;

                using (SmtpClient client = new SmtpClient(_address, _port))
                {
                    try
                    {
                        client.Send(email);
                    }
                    catch (ArgumentNullException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString(), "ArgumentNullException");
                        _numFails++;
                    }
                    catch (SmtpException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString(), "SmtpException");
                        _numFails++;
                    }
                }
            }
        }

        protected override void Connect() { }

        protected override void Close() { } // no smtp close
    }
}