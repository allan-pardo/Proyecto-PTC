using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEntidades;
using SistemaDatos;

namespace SistemaNegocio
{
    public class SN_Proovedor
    {

        private SD_Proovedor objcd_Proveedor = new SD_Proovedor();


        public List<Proovedor> Listar()
        {
            return objcd_Proveedor.Listar();
        }

        public int Registrar(Proovedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }

            if (obj.razonSocial == "")
            {
                Mensaje += "Es necesario la razon social del Proveedor\n";
            }

            if (obj.correo == "")
            {
                Mensaje += "Es necesario la correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Proveedor.Registrar(obj, out Mensaje);
            }
        }


        public bool Editar(Proovedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }

            if (obj.razonSocial == "")
            {
                Mensaje += "Es necesario la razon social del Proveedor\n";
            }

            if (obj.correo == "")
            {
                Mensaje += "Es necesario la correo del Proveedor\n";
            }



            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Proveedor.Editar(obj, out Mensaje);
            }
        }


        public bool Eliminar(Proovedor obj, out string Mensaje)
        {
            return objcd_Proveedor.Eliminar(obj, out Mensaje);
        }



    }
}
