using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using DAO_Controlador.ManagerController;
using Modelo.Modelos;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(ILogger<ProductoController> logger,
        IRepositorioGenerico<Producto> producto_) : Controller
    {
        private readonly ILogger<ProductoController> _logger = logger;
        private readonly IRepositorioGenerico<Producto> repositorioGenerico = producto_;
        [HttpGet]
        public async Task<List<Producto>> Listar()
        {
            List<Producto> _listProduto = await repositorioGenerico.ObtenerTodos();
            return _listProduto;
        }
        [HttpGet]
        [Route("ObtenerPorId/")]
        public async Task<Producto> ObtnerPorId(int id_Producto)
        {
            Producto _Produto = await repositorioGenerico.ObtenerXId(id_Producto);
            return _Produto;
        }

        [HttpPost]
        [Route("crearProducto/")]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            bool respuesta = await repositorioGenerico.Crear(producto);
            return Ok(new { respuesta });
        }

        [HttpPut]
        [Route("Actualizar/")]
        public async Task<IActionResult> Actualizar([FromBody] Producto producto)
        {
            bool respuesta = await repositorioGenerico.Actualizar(producto);
            return Ok(new { respuesta });
        }


        [HttpDelete]
        [Route("Eliminar/")]
        public async Task<IActionResult> Eliminar(int id_Producto)
        {
            bool respuesta = await repositorioGenerico.Borrar(id_Producto);
            return Ok(new { respuesta });
        }


    }
}
