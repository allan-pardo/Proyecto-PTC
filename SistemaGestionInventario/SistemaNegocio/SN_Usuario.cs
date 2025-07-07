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

        public List<Usuario> lister()
        {
            return objsd_usuario.lister();
        }

    }
}
