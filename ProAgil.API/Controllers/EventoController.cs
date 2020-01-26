using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Data;
using ProAgil.API.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok( await _context.Eventos.ToListAsync());
            }
            catch (System.Exception)
            {                
                throw new Exception("A Requisição ao banco de dados falhou ! :( ");
            }            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            try
            {
                return Ok(await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id));                
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}