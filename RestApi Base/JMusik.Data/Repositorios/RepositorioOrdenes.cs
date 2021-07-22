using JMusik.Data.Contratos;
using JMusik.Models;
using JMusik.Models.Enum;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMusik.Data.Repositorios
{
    public class RepositorioOrdenes : IOrdenesRepositorio
    {
        private readonly TiendaDbContext _contexto;
        private readonly ILogger<RepositorioPerfiles> _logger;
        private DbSet<Orden> _dbSet;

        public RepositorioOrdenes(TiendaDbContext contexto, ILogger<RepositorioPerfiles> logger)
        {
            this._contexto = contexto;
            this._logger = logger;
            this._dbSet = _contexto.Set<Orden>();
        }// fin del constructor

        public async Task<bool> Actualizar(Orden entity)
        {
            _dbSet.Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
            try
            {
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Actualizar)}: " + excepcion.Message);
            }
            return false;
        }// fin del metodo

        public async Task<Orden> Agregar(Orden entity)
        {
            entity.EstatusOrden = EstatusOrden.Activo;
            entity.FechaRegistro = DateTime.Now;            
            _dbSet.Add(entity);
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: " + excepcion.Message);
                return null;
            }
            return entity;
        }// fin del metodo

        public async Task<bool> Eliminar(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.Id == id);
            entity.EstatusOrden = EstatusOrden.Inactivo;            
            try
            {
                return (await _contexto.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: " + excepcion.Message);
            }
            return false;
        }// fin del metodo

        public async Task<Orden> ObtenerAsync(int id)
        {
            return await _dbSet.Include(orden=>orden.Usuario)                    
                                .SingleOrDefaultAsync(c => c.Id == id 
                                && c.EstatusOrden == EstatusOrden.Activo);
        }// fin del metodo

        public async Task<Orden> ObtenerConDetallesAsync(int id)
        {
            return await _dbSet.Include(orden => orden.Usuario)
                                .Include(orden=>orden.DetalleOrden)
                                    .ThenInclude(detalleOrden => detalleOrden.Producto)
                                .SingleOrDefaultAsync(c => c.Id == id 
                                && c.EstatusOrden == EstatusOrden.Activo);
        }// fin del metodo

        public async Task<IEnumerable<Orden>> ObtenerTodosAsync()
        {
            return await _dbSet.Where(u=>u.EstatusOrden== EstatusOrden.Activo)
                                .Include(orden => orden.Usuario)                                                               
                                .ToListAsync();
        }// fin del metodo

        public async Task<IEnumerable<Orden>> ObtenerTodosConDetallesAsync()
        {
            return await _dbSet.Where(u => u.EstatusOrden == EstatusOrden.Activo)
                                .Include(orden => orden.Usuario)
                                .Include(orden => orden.DetalleOrden)
                                    .ThenInclude(detalleOrden => detalleOrden.Producto)
                                .ToListAsync();
        } // fin del metodo 

    }// fin de la clase RepositorioOrdenes
}// fin del namespace
