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

        List<Permiso> lista = new List<Permiso>();

        using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
        {
            try
            {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.idRol,p.nombreMenu from Permiso p");
                    query.AppendLine("inner join Rol r on r.idRol = p.idRol");
                    query.AppendLine("inner join Usuario u on u.idRol = r.idRol");
                    query.AppendLine("where u.idUsuario = 4");

                SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        lista.Add(new Permiso()
                        {
                            oRol = new Rol() {idRol = Convert.ToInt32(dr["idUsuario"]) } ,
                            nombreMenu = dr["nombreMenu"].ToString(),

                        });
                    }

                }



            }
            catch (Exception ex)
            {
                lista = new List<Permiso>();
            }

                }
        return lista;
        }
    }
}

