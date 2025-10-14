using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

using SistemaDatos;
using SistemaEntidades;

namespace SistemaNegocio
{
    public class SN_Usuario
    {
        private readonly SD_Usuario dao = new SD_Usuario();

        public bool HayUsuarios() => dao.HayUsuarios();

        private SD_Usuario objsd_usuario = new SD_Usuario();

        // ===== LISTAR =====
        public List<Usuario> listar()
        {
            return objsd_usuario.listar();
        }

        
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.documento))
                Mensaje += "Es necesario el documento del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.nombreCompleto))
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.clave))
                Mensaje += "Es necesaria la clave del usuario\n";

            if (Mensaje != string.Empty)
                return 0;

            // Hash antes de guardar
            obj.clave = BCrypt.Net.BCrypt.HashPassword(obj.clave);



            return objsd_usuario.Registrar(obj, out Mensaje);

        
        }

        
        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.documento))
                Mensaje += "Es necesario el documento del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.nombreCompleto))
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (!string.IsNullOrEmpty(Mensaje))
                return false;

            // ¿El admin escribió nueva contraseña?
            bool cambiaClave = !string.IsNullOrWhiteSpace(obj.clave);

            if (cambiaClave)
            {
                // Nueva clave: hasheamos
                obj.clave = BCrypt.Net.BCrypt.HashPassword(obj.clave);
            }
            else
            {
                // No cambió la clave: conservar hash actual
                var actual = objsd_usuario.ObtenerPorDocumento(obj.documento);
                if (actual == null)
                {
                    Mensaje = "No se encontró el usuario para actualizar.";
                    return false;
                }
                obj.clave = actual.clave; // mantén el hash existente
            }

            // Llama a tu SD_Usuario.Editar habitual
            return objsd_usuario.Editar(obj, out Mensaje);
        }

        
        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objsd_usuario.Eliminar(obj, out Mensaje);
        }

        // ===== LOGIN (documento + BCrypt) =====
        public Usuario Login(string documento, string clavePlano)
        {
            var u = objsd_usuario.ObtenerPorDocumento(documento);
            if (u == null) return null;

            string hash = u.clave ?? "";
            bool esBCrypt = hash.StartsWith("$2a$") || hash.StartsWith("$2b$") || hash.StartsWith("$2y$");

            if (esBCrypt)
                return BCrypt.Net.BCrypt.Verify(clavePlano, hash) ? u : null;

            // Migración de contraseñas en texto plano (si quedara alguna)
            if (hash == clavePlano)
            {
                string nuevoHash = BCrypt.Net.BCrypt.HashPassword(clavePlano);
                objsd_usuario.ActualizarClave(u.idUsuario, nuevoHash);
                u.clave = nuevoHash;
                return u;
            }
            return null;
        }

    }
}
