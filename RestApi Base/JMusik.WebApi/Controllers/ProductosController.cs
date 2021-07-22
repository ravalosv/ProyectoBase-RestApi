using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMusik.Data;
using JMusik.Models;
using JMusik.Data.Contratos;
using AutoMapper;
using JMusik.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using JMusik.WebApi.Helpers;

// LA IMPLEMENTACION DE TODOS ESTOS METODOS ESTAN EN RepositorioProductos

namespace JMusik.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]  // atributo de enrutamiento
    [ApiController]        // para usar el enrutamiento de atributo
    public class ProductosController : ControllerBase   // para poder usar WebApi
    {
        private IProductosRepositorio _productosRepositorio;
        private readonly IMapper _mapper;   // aqui se declara el campo mapper
        private readonly ILogger<ProductosController> _logger; // aqui se declara el campo logger

        // aqui se agrega el campo mapper
        // aqui se agrega el campo logger
        public ProductosController(IProductosRepositorio productosRepositorio, IMapper mapper, ILogger<ProductosController>logger)
        {
            _productosRepositorio = productosRepositorio;
            this._mapper = mapper;
            this._logger = logger;
        } // fin del constructor



        //// GET: api/Productos   // obtener todos los productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Paginador<ProductoDto>>> Get(int paginaActual = 1, int registrosPorPagina = 3)
        {
            try
            {
                var resultado = await _productosRepositorio.ObtenerPaginasProductosAsync(paginaActual, registrosPorPagina);

                var listaProductosDto = _mapper.Map<List<ProductoDto>>(resultado.registros);

                return new Paginador<ProductoDto>(listaProductosDto, resultado.totalRegistros, paginaActual, registrosPorPagina);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: ${ex.Message}");
                return BadRequest();
            }
        }


        // GET: api/Productos/5   // consultar un producto
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            try
            {
                var producto = await _productosRepositorio.ObtenerProductoAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }// fin del if
                return _mapper.Map<ProductoDto>(producto);
            }// fin del try
            catch (Exception ex)
            {
                return NotFound(ex);
            }// fin del catch

        } // fin del metodo

        // POST: api/Productos  // crear producto
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Post(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);

                var nuevoProducto = await _productosRepositorio.Agregar(producto);

                if (nuevoProducto == null)
                {
                    return BadRequest();
                }// fin del if

                var nuevoProductoDto = _mapper.Map<ProductoDto>(nuevoProducto) ;
                return CreatedAtAction(nameof(Post), new { id = nuevoProductoDto.Id }, nuevoProductoDto);

            }// fin del try
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: ${ex.Message}");

                return BadRequest(ex);

            }// fin del catch
        }// fin del metodo


        // PUT: api/Productos/5   // actualizar producto

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto productoDto)
        {
            try
            {
                if (productoDto == null)
                {
                    return NotFound();
                }// fin del if

                var producto = _mapper.Map<Producto>(productoDto);

                var resultado = await _productosRepositorio.Actualizar(producto);
                if (!resultado)
                {
                    return BadRequest();
                }// fin del if 2
                return productoDto;

            }// fin del try
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Put)}: ${ex.Message}");
                return BadRequest(ex);
            }
        }// fin del metodo


        // DELETE: api/Productos/5   // borrar producto

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)   // se usa IActionResult, ya que no devuelve nada
        {
            try
            {
                var resultado = await _productosRepositorio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }// fin del if
                return NoContent();
            }// fin del try
            catch(Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: ${ex.Message}");
                return BadRequest(ex);
            }
        }// fin del metodo
       
    }// fin de la clase ProductosController

}// fin del namespace
