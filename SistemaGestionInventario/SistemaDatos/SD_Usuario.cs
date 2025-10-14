using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using SistemaEntidades;
using System.Collections;
using System.Security.Claims;
using System.Xml.Linq;
using System.Windows.Forms;

namespace SistemaDatos
{
    public class SD_Usuario
    {

        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.idUsuario,u.Documento,u.nombreCompleto,u.correo,u.clave,u.estado,r.idRol,r.descripcion from Usuario u");
                    query.AppendLine("inner join Rol r on r.idRol= u.idRol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while  (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                idUsuario = Convert.ToInt32(dr["idUsuario"]),
                                documento = dr["Documento"].ToString(),
                                nombreCompleto = dr["nombreCompleto"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                oRol = new Rol()
                                {
                                    idRol = Convert.ToInt32(dr["idRol"]),
                                    descripcion = dr["descripcion"].ToString()
                                }
                            });
                        }

                    }



                }
                catch (Exception ex)
                {
                    lista = new List<Usuario>();
                }

            }
            return lista;
        }


        public int Registrar(Usuario obj , out string Mensaje)
        {

            int idUsuarioGenerado = 0;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTROUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("Documento",obj.documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.nombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.correo);
                    cmd.Parameters.AddWithValue("Clave", obj.clave);
                    cmd.Parameters.AddWithValue("idRol", obj.oRol.idRol);
                    cmd.Parameters.AddWithValue("Estado", obj.estado);
                    cmd.Parameters.Add("idUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idUsuarioGenerado = Convert.ToInt32 (cmd.Parameters["idUsuarioResultado"].Value) ;
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                idUsuarioGenerado = 0;
                Mensaje = ex.Message;
            }


            return idUsuarioGenerado;
            

        }

        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;
            using (var cn = new SqlConnection(Conexion.cadena))
            {
                cn.Open();
                var cmd = new SqlCommand(
                    @"UPDATE Usuario
              SET Documento=@doc, nombreCompleto=@nom, correo=@cor,
                  clave=@clave, idRol=@rol, estado=@est
              WHERE idUsuario=@id", cn);

                cmd.Parameters.AddWithValue("@doc", obj.documento);
                cmd.Parameters.AddWithValue("@nom", obj.nombreCompleto);
                cmd.Parameters.AddWithValue("@cor", obj.correo);
                cmd.Parameters.AddWithValue("@clave", obj.clave); // ya viene hash o el actual
                cmd.Parameters.AddWithValue("@rol", obj.oRol.idRol);
                cmd.Parameters.AddWithValue("@est", obj.estado);
                cmd.Parameters.AddWithValue("@id", obj.idUsuario);

                try { return cmd.ExecuteNonQuery() > 0; }
                catch (Exception ex) { Mensaje = ex.Message; return false; }
            }

        }


        public bool Eliminar(Usuario obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("idUsuario", obj.idUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;

        }

        public Usuario ObtenerPorDocumento(string documentoOEmail)
        {
            using (var cn = new SqlConnection(Conexion.cadena))
            using (var cmd = new SqlCommand(@"
            SELECT TOP 1 
            u.idUsuario,u.documento,u.nombreCompleto,u.correo,u.clave,u.estado,
            u.idRol, r.descripcion
            FROM Usuario u
            LEFT JOIN Rol r ON r.idRol = u.idRol            
            WHERE LTRIM(RTRIM(u.documento)) = LTRIM(RTRIM(@doc));", cn)) // trim en ambos lados
            {
                cmd.Parameters.AddWithValue("@doc", documentoOEmail ?? "");
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new Usuario
                        {
                            idUsuario = Convert.ToInt32(dr["idUsuario"]),
                            documento = dr["documento"].ToString(),
                            nombreCompleto = dr["nombreCompleto"].ToString(),
                            correo = dr["correo"].ToString(),
                            clave = dr["clave"].ToString(),
                            estado = Convert.ToBoolean(dr["estado"]),
                            oRol = new Rol
                            {
                                idRol = Convert.ToInt32(dr["idRol"]),
                                descripcion = dr["descripcion"]?.ToString()
                            }
                        };
                    }
                }
            }
            return null;
        }

        // Útil para migración o para cuando el admin cambie la clave:
        public void ActualizarClave(int idUsuario, string nuevoHash)
        {
            using (var cn = new SqlConnection(Conexion.cadena))
            using (var cmd = new SqlCommand("UPDATE Usuario SET clave=@h WHERE idUsuario=@id", cn))
            {
                cmd.Parameters.Add("@h", SqlDbType.VarChar, 60).Value = nuevoHash;
                cmd.Parameters.AddWithValue("@id", idUsuario);
                cn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public bool HayUsuarios()
        {
            using (var cn = new SqlConnection(Conexion.cadena))
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Usuario", cn))
            { cn.Open(); return (int)cmd.ExecuteScalar() > 0; }
        }

    }
}
