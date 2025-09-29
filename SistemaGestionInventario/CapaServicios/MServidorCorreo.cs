using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace SistemaDatos.ServicioCorreo
{
    public abstract class MServidorCorreo : IDisposable
    {
        private SmtpClient smtpClient;
        private string sendermail;
        private string password;
        private string host;
        private int port;
        private bool ssl;
        private string displayName;

        // Propiedades protegidas: configurables por la clase hija
        protected string Sendermail { get => sendermail; set => sendermail = value; }
        protected string Password { get => password; set => password = value; }
        protected string Host { get => host; set => host = value; }
        protected int Port { get => port; set => port = value; }
        protected bool Ssl { get => ssl; set => ssl = value; }
        protected string DisplayName { get => displayName; set => displayName = value; }

        protected void InitializaSmtpClient()
        {
            // Usa las propiedades (no valores hardcodeados)
            smtpClient = new SmtpClient(Host, Port)
            {
                EnableSsl = Ssl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Sendermail, Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 15000
            };
        }

        public bool EnviarMail(string subject, string body, List<string> recipientMail, bool isBodyHtml = true)
        {
            using (var mailMessage = new MailMessage())
            {
                try
                {
                    mailMessage.From = new MailAddress(Sendermail, DisplayName);
                    foreach (string mail in recipientMail)
                        mailMessage.To.Add(mail);

                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = isBodyHtml;
                    mailMessage.Priority = MailPriority.Normal;

                    smtpClient.Send(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR DE EMAIL: {ex}", "Error detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public void Dispose()
        {
            smtpClient?.Dispose();
            smtpClient = null;
        }
    }

}
