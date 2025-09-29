using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SistemaDatos.ServicioCorreo
{
    public class SoporteCorreo : MServidorCorreo
    {
        public SoporteCorreo()
        {
            // Carga desde App.config
            Sendermail = ConfigurationManager.AppSettings["Mail.From"];
            Password = ConfigurationManager.AppSettings["Mail.AppPassword"];
            Host = ConfigurationManager.AppSettings["Mail.Host"];
            Port = int.Parse(ConfigurationManager.AppSettings["Mail.Port"]);
            Ssl = bool.Parse(ConfigurationManager.AppSettings["Mail.EnableSsl"]);
            DisplayName = ConfigurationManager.AppSettings["Mail.DisplayName"];

            InitializaSmtpClient();
        }
    }
}
