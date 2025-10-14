using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaDatos;
using SistemaEntidades;

namespace SistemaNegocio
{
    public class SN_Permiso
    {
        private readonly SD_Permiso dao = new SD_Permiso();
        public List<Permiso> listar(int idUsuario) => dao.listar(idUsuario);
    }
}
