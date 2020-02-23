using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public EventoController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok( await _repo.GetAllEventoAsync(true));
            }
            catch (System.Exception)
            {        
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou - método GETALL");                       
            }            
        }

        [HttpGet("{Eventoid}")]
        public async Task<ActionResult<Evento>> Get(int Eventoid)
        {
            try
            {
                return Ok(await _repo.GetEventoAsyncById(Eventoid, true));                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou - método GETBYID"); 
            }
        }

        [HttpGet("getByTema/{Tema}")]
        public async Task<ActionResult<Evento>> Get(string tema)
        {
            try
            {
                return Ok(await _repo.GetAllEventoAsyncByTema(tema, true));                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou - método GETBYTEMA"); 
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangeAsync())
                    return Created($"/api/evento/{model.EventoId}", model);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou - método POST"); 
            }

            return BadRequest();
        }

         [HttpPut("{Eventoid}")]
        public async Task<ActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if (evento == null)
                    return NotFound();                    
                
                _repo.Update(model);

                if(await _repo.SaveChangeAsync())
                    return Created($"/api/evento/{model.EventoId}", model);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou - método PUT"); 
            }

            return BadRequest();
        }

       [HttpDelete("{Eventoid}")]
        public async Task<ActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if (evento == null)
                    return NotFound();                    
                
                _repo.Delete(evento);

                if(await _repo.SaveChangeAsync())
                    return Ok();
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou - método DELETE"); 
            }

            return BadRequest();
        }
    }
}