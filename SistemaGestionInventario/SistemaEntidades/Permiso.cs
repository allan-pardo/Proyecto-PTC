using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEntidades
{
    public class Permiso
    {
        public int idPermiso { get; set; }
        public Rol oRol { get; set; } = new Rol();
        public string nombreMenu { get; set; }
        public DateTime fechaRegistro { get; set; }   // o 
    }
}
