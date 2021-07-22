using JMusik.Models;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace JMusik.Data.Contratos
{

    // las referencias se crean automaticamente con waipro + 2 tabs desde los codesnipes
    public interface IProductosRepositorio
    {
        Task<List<Producto>> ObtenerProductosAsync();

        Task<(int totalRegistros, IEnumerable<Producto> registros)> ObtenerPaginasProductosAsync(int paginaActual, int registrosPorPagina);
        Task<Producto> ObtenerProductoAsync(int id);
        Task<Producto> Agregar(Producto producto);
        Task<bool> Actualizar(Producto producto);
        Task<bool> Eliminar(int id);
    }// fin de la interface IProductosRepositorio

}// fin del namespace


