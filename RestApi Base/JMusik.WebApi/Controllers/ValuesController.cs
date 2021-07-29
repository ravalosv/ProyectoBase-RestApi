using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]  // atributo de enrutamiento
    [ApiController]        // para usar el enrutamiento de atributo
    public class ValuesController : ControllerBase  // para usar webApi
    {
        // GET api/values     // obtener
        [HttpGet]
        public ActionResult<IEnumerable<string>> GET()
        {
            return new string[] { "value1", "value2", "value3", "value4", "value5", "value6" };
        }

        // GET api/values/5   //leer
        [HttpGet("{id}")]
        public ActionResult<string> GET(int id)
        {
            return "value";
        }

        // POST api/value   //crear
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/value/5      // 
        [HttpPut ("{id}")]
        public void Put(int id,[FromBody] string value)
        {

        }

        // delete api/value/5
        [HttpDelete ("{id}")]
        public void Delete(int id)
        {

        }

    }// fin de la clase ValuesController
}// fin del namespace
