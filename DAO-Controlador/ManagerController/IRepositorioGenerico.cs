using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Controlador.ManagerController
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<List<T>> ObtenerTodos();
        Task<T> ObtenerXId(int id);
        Task<bool> Crear(T modelo);
        Task<bool> Actualizar(T modelo);
        Task<bool> Borrar(int id);
    }
}
