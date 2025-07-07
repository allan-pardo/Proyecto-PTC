﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEntidades
{
    internal class Venta
    {
        public int idVenta { get; set; }
        public Usuario oUsuario { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string documentoCliente { get; set; }
        public string nombreCliente { get; set; }
        public decimal montoPago { get; set; }
        public decimal montoCambio { get; set; }
        public decimal montoTotal { get; set; }
        public List<Detalle_Venta> oDetalle_Venta { get; set; }
        public string fechaRegistro { get; set; }
    }
}
