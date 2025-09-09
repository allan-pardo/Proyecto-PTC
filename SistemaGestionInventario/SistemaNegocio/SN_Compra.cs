using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEntidades;
using SistemaDatos;

namespace SistemaNegocio
{
    public class SN_Compra
    {

        private SD_Compra objcd_compra = new SD_Compra();


        public int ObtenerCorrelativo()
        {
            return objcd_compra.ObtenerCorrelativo();
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            return objcd_compra.Registrar(obj, DetalleCompra, out Mensaje);
        }

        public Compra ObtenerCompra(string numero)
        {

            Compra oCompra = objcd_compra.ObtenerCompra(numero);

            if (oCompra.idCompra != 0)
            {
                List<Detalle_Compra> oDetalleCompra = objcd_compra.ObtenerDetalleCompra(oCompra.idCompra);

                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }


    }
}
