using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using JMusik.Data.Contratos;
using JMusik.Dtos;
using JMusik.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private IRepositorioGenerico<Perfil> _perfilesRepositorio;
        private readonly ILogger<PerfilesController> _logger;
        private readonly IMapper _mapper;

        public PerfilesController(IRepositorioGenerico<Perfil> perfilesRepositorio, ILogger<PerfilesController> logger,IMapper mapper)
        {
            this._perfilesRepositorio = perfilesRepositorio;
            this._logger = logger;
            this._mapper = mapper;
        }

        //// GET: api/perfiles   // consultar todos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> Get()
        {
            try
            {
                var perfiles= await _perfilesRepositorio.ObtenerTodosAsync();
                return _mapper.Map<List<PerfilDto>>(perfiles);
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Get)}: " + excepcion.Message);
                return BadRequest();
            }
        } // fin del metodo obtener todos los datos

        // GET: api/perfiles/5   //consultar uno
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerfilDto>> Get(int id)
        {
            var perfil = await _perfilesRepositorio.ObtenerAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }
            return _mapper.Map<PerfilDto>(perfil);
        } // fin del metodo obtener 1 solo dato

        // POST: api/perfiles // crear
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PerfilDto>> Post(PerfilDto perfilDto)
        {
            try
            {
                var perfil = _mapper.Map<Perfil>(perfilDto);

                var nuevoPerfil = await _perfilesRepositorio.Agregar(perfil);
                if (nuevoPerfil == null)
                {
                    return BadRequest();
                }

                var nuevoPerfilDto = _mapper.Map<PerfilDto>(nuevoPerfil);
                return CreatedAtAction(nameof(Post), new { id = nuevoPerfilDto.Id }, nuevoPerfilDto);

            }
            catch (Exception excepcion)            
            {
                _logger.LogError($"Error en {nameof(Post)}: " + excepcion.Message);
                return BadRequest();
            }
        }// fin del metodo Crear

        // PUT: api/perfiles/5    // actualizar
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PerfilDto>> Put(int id, [FromBody]PerfilDto perfilDto)
        {
            if (perfilDto == null)
                return NotFound();


            var perfil = _mapper.Map<Perfil>(perfilDto);
            var resultado = await _perfilesRepositorio.Actualizar(perfil);
            if (!resultado)
                return BadRequest();

            return perfilDto;
        }// fin del metodo Actualizar

        // DELETE: api/perfiles/5  // borrar
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _perfilesRepositorio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Delete)}: " + excepcion.Message);
                return BadRequest();
            }
        } // fin del metodo Delete


    }// fin de la clase PerfilesController
}// fin del namespace