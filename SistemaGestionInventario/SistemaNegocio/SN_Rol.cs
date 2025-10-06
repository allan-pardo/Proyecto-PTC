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
        private readonly SD_Rol dao = new SD_Rol();

        public List<Rol> listar()
        {
            return objsd_rol.listar();
        }

        
        public int EnsureRol(string nombre)
        {
            int id = dao.ObtenerIdRolPorNombre(nombre);
            if (id == 0) id = dao.CrearRol(nombre);
            return id;
        }
    }
}
