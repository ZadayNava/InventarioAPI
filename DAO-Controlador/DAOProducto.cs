using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Controlador.ManagerController;
using Modelo;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modelo.Modelos;
using System.Data;
using System.Reflection;

namespace DAO_Controlador
{
    public class DAOProducto : IRepositorioGenerico<Producto>
    {
        private readonly string cadenaConexion = string.Empty;

        public DAOProducto(IConfiguration configuration)
        {
            cadenaConexion = configuration.GetConnectionString("InventarioConection").ToString();
        }   

        public async Task<bool> Actualizar(Producto modelo)
        {
            bool respuesta = false;

            try
            {
                using (var objConexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_ActualizarProducto", objConexion);
                    cmd.Parameters.AddWithValue("id_Producto", modelo.id_Producto);
                    cmd.Parameters.AddWithValue("nombre", modelo.Nombre);
                    cmd.Parameters.AddWithValue("descripcion", modelo.Descripcion);
                    cmd.Parameters.AddWithValue("cantidad", modelo.Cantidad);
                    cmd.Parameters.Add("respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;


                    await objConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    respuesta = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
                }
            }
            catch (Exception ex) 
            { 
                respuesta = false;
            }

            return respuesta;
        }

        public async Task<bool> Borrar(int id_Producto)
        {
            bool respuesta = false;

            try
            {
                using (var objConexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_Eliminar", objConexion);
                    cmd.Parameters.AddWithValue("id_Producto", id_Producto);
                    cmd.Parameters.Add("respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;


                    await objConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    respuesta = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }

            return respuesta;
        }

        public async Task<bool> Crear(Producto modelo)
        {
            bool respuesta = false;

            try
            {
                using (var objConexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_CrearProducto", objConexion);
                    cmd.Parameters.AddWithValue("nombre", modelo.Nombre);
                    cmd.Parameters.AddWithValue("descripcion", modelo.Descripcion);
                    cmd.Parameters.AddWithValue("cantidad", modelo.Cantidad);
                    cmd.Parameters.Add("respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    await objConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    respuesta = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }

            return respuesta;
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            List<Producto> listaProducto = new List<Producto>();
            try
            {
                SqlDataReader lector;
                using (var objConexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_ListarTodo", objConexion)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    await objConexion.OpenAsync();
                    using (lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            listaProducto.Add(
                                    new Producto
                                    {
                                        id_Producto = Convert.ToInt32(lector["id_Producto"].ToString()),
                                        Nombre = lector["Nombre"].ToString(),
                                        Descripcion = lector["Descripcion"].ToString(),
                                        Cantidad = Convert.ToInt32(lector["Cantidad"].ToString())
                                    }
                                );
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                listaProducto = new List<Producto>();
            }
            return listaProducto;
        }

        public async Task<Producto> ObtenerXId(int id_Producto)

        {
            Producto _producto = new Producto();
            try
            {
                SqlDataReader lector;
                using (var objConexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("SP_ObtenerProducto", objConexion)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("id_Producto", id_Producto);

                    await objConexion.OpenAsync();
                    using (lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            _producto = new Producto
                            {
                                id_Producto = Convert.ToInt32(lector["id_Producto"].ToString()),
                                Nombre = lector["Nombre"].ToString(),
                                Descripcion = lector["Descripcion"].ToString(),
                                Cantidad = Convert.ToInt32(lector["Cantidad"].ToString())
                            };
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                _producto = new Producto();
            }
            return _producto;
        }
    }
}
