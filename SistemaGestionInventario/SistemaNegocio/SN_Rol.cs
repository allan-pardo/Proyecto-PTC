using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDatos;
using SistemaEntidades;

namespace SistemaNegocio
{
    public class SN_Rol
    {

        private SD_Rol objsd_rol = new SD_Rol();

        public List<Rol> listar()
        {
            return objsd_rol.listar();
        }

    }
}
