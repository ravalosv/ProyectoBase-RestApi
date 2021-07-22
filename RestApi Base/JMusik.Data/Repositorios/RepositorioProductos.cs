using JMusik.Data.Contratos;
using JMusik.Models;
using JMusik.Models.Enum;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repositorios
{

    public class RepositorioProductos : IProductosRepositorio
    {
        private TiendaDbContext _contexto;
        private readonly ILogger<RepositorioProductos> _logger; // se agrega el logger

        // aqui se agrega el logger
        public RepositorioProductos(TiendaDbContext contexto, ILogger<RepositorioProductos>logger)
        {
            _contexto = contexto;
            _logger = logger;
        }
        public async Task<bool> Actualizar(Producto producto)
        {
            var productoBD = await ObtenerProductoAsync(producto.Id);
            productoBD.Nombre = producto.Nombre;
            productoBD.Precio = producto.Precio;
            productoBD.FechaRegistro = DateTime.Now;

            //_contexto.Productos.Attach(producto);
            //_contexto.Entry(producto).State = EntityState.Modified;

            try
            {
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }// fin del try
            catch (Exception excepcion)
            {
               _logger.LogError($"Error en {nameof(Actualizar)}: {excepcion.Message}");
                return false;
            }// fin del catch
        } // fin del metodo

        public async Task<Producto> Agregar(Producto producto)
        {

            producto.Estatus = EstatusProducto.Activo;
            producto.FechaRegistro = DateTime.Now;

            _contexto.Productos.Add(producto);
            try
            {
                await _contexto.SaveChangesAsync();
            }// fin del try
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: {excepcion.Message}");
                return null;
            }// fin del catch

            return producto;
        }// fin del metodo

        public async Task<bool> Eliminar(int id)
        {
            //Se realiza una eliminación suave, solamente inactivamos el producto

            var producto = await _contexto.Productos
                                .SingleOrDefaultAsync(c => c.Id == id);

            producto.Estatus = EstatusProducto.Inactivo;
            _contexto.Productos.Attach(producto);
            _contexto.Entry(producto).State = EntityState.Modified;

            try
            {
                return (await _contexto.SaveChangesAsync() > 0 ? true : false);
            }// fin del try
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: {excepcion.Message}");
            }// fin del catch
            return false;

        }// fin del metodo

        public async Task<Producto> ObtenerProductoAsync(int id)
        {
            return await _contexto.Productos
                               .SingleOrDefaultAsync(c => c.Id == id && c.Estatus == EstatusProducto.Activo);
        } // fin del metodo

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _contexto.Productos
                              .Where(u => u.Estatus == EstatusProducto.Activo)
                              .OrderBy(u => u.Nombre)
                              .ToListAsync();
        }// fin del metodo


        public async Task<(int totalRegistros, IEnumerable<Producto> registros)> ObtenerPaginasProductosAsync(int paginaActual, int registrosPorPagina)
        {
            var totalRegistros = await _contexto.Productos
                .Where(u => u.Estatus == EstatusProducto.Activo)
                .CountAsync();

            var registros = await _contexto.Productos
                .Where(u => u.Estatus == EstatusProducto.Activo)
                .Skip((paginaActual - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            return (totalRegistros, registros);
        }

    }// fin de la clase

}// fin del namespace