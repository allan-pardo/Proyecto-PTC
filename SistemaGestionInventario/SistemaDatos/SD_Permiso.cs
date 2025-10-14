using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Data;
using System.Data.SqlClient;
using SistemaEntidades;
using System.Reflection;

namespace SistemaDatos
{
    public class SD_Permiso
    {
        public List<Permiso> listar(int idUsuario)
        {
            var lista = new List<Permiso>();

            using (SqlConnection cn = new SqlConnection(Conexion.cadena))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT p.idPermiso, p.nombreMenu, r.idRol, r.descripcion
            FROM Permiso p
            INNER JOIN Rol r   ON r.idRol  = p.idRol
            INNER JOIN Usuario u ON u.idRol = r.idRol
            WHERE u.idUsuario = @idUsuario;", cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Permiso
                        {
                            idPermiso = Convert.ToInt32(dr["idPermiso"]),
                            nombreMenu = dr["nombreMenu"].ToString(),
                            oRol = new Rol
                            {
                                idRol = Convert.ToInt32(dr["idRol"]),
                                descripcion = dr["descripcion"].ToString()
                            }
                        });
                    }
                }
            }
            return lista;
        }
    }
}

