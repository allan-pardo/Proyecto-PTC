using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDatos;
using SistemaDatos.ServicioCorreo;
using System.Security.Cryptography;

namespace SistemaNegocio
{
    public class ServicioRecuperacionContraseña
    {
        private readonly SD_Usuario _dao = new SD_Usuario();

        private string GenerarPasswordTemporal(int length = 10)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@$%#?";

            // En .NET Framework: crear instancia y rellenar el buffer
            var bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[bytes[i] % chars.Length]);
            }
            return sb.ToString();

        }

        public string RecuperarContra(string documentoOCorreo)
        {
            try
            {
                var u = _dao.ObtenerPorDocumento((documentoOCorreo ?? string.Empty).Trim());

                // Respuesta neutra si no existe o no tiene correo
                if (u == null || string.IsNullOrWhiteSpace(u.correo))
                    return "Si existe una cuenta asociada, se enviará un correo con instrucciones.";

                // 1) generar temporal y hashear
                string temporalPlano = GenerarPasswordTemporal(10);
                string temporalHash = BCrypt.Net.BCrypt.HashPassword(temporalPlano); // requiere BCrypt.Net-Next

                // 2) guardar hash
                _dao.ActualizarClave(u.idUsuario, temporalHash);

                // 3) enviar correo (sintaxis using clásica)
                bool ok;
                using (var mailer = new SoporteCorreo())
                {
                    ok = mailer.EnviarMail(
                        subject: "Recuperación de contraseña",
                        body:
                        @"Hola " + u.nombreCompleto + @",

                        Se generó una contraseña temporal para tu cuenta:

                        " + temporalPlano + @"

                        Puedes iniciar sesión con ella y luego cambiarla desde tu perfil.

                        Si no solicitaste esto, ignora este mensaje.",
                        recipientMail: new List<string> { u.correo },
                        isBodyHtml: false
                    );
                }

                return ok
                    ? "Si existe una cuenta asociada, se envió un correo con instrucciones."
                    : "No se pudo enviar el correo. Intenta nuevamente más tarde.";
            }
            catch
            {
                return "No se pudo completar la solicitud en este momento.";
            }
        }
    }
}
