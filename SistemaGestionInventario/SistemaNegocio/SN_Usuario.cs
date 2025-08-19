using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaDatos;
using SistemaEntidades;

namespace SistemaNegocio
{
    public class SN_Usuario
    {

        private SD_Usuario objsd_usuario = new SD_Usuario();

        public List<Usuario> listar()
        {
            return objsd_usuario.listar();
        }



        public int Registrar(Usuario obj, out string Mensaje) 
        {
            Mensaje = string.Empty;

            if(obj.documento == "")
            {
                Mensaje += "Es necesario  el documento del usuario";
            }

            if (obj.nombreCompleto == "")
            {
                Mensaje += "Es necesario  el nombre completo del usuario";
            }

            if (obj.clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario";
            }

            if(Mensaje != string.Empty)
            {
                return  0;
            }
            else
            {
                return objsd_usuario.Registrar(obj, out Mensaje);
            }
            

            
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario  el documento del usuario";
            }

            if (obj.nombreCompleto == "")
            {
                Mensaje += "Es necesario  el nombre completo del usuario";
            }

            if (obj.clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objsd_usuario.Editar(obj, out Mensaje);
            }

            
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objsd_usuario.Eliminar(obj, out Mensaje);
        }
    }
}
