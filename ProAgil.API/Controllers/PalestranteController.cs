using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public PalestranteController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet("{PalestranteId}")]
        public async Task<ActionResult> Get(int PalestranteId)
        {
           try
           {
               return Ok(await _repo.GetAllPalestranteAsync(PalestranteId, true));
           }
           catch (System.Exception)
           {
               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
           } 
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
           try
           {
               return Ok(await _repo.GetAllPalestranteAsyncByName(name, true));
           }
           catch (System.Exception)
           {
               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
           } 
        }

        [HttpPost]
        public async Task<ActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangeAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou"); 
            }

            return BadRequest();
        }

         [HttpPut]
        public async Task<ActionResult> Put(int PalestranteId, Palestrante model)
        {
            try
            {
                var evento = await _repo.GetAllPalestranteAsync(PalestranteId, false);
                if (evento == null)
                    return NotFound();                    
                
                _repo.Update(model);

                if(await _repo.SaveChangeAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou"); 
            }

            return BadRequest();
        }

       [HttpDelete]
        public async Task<ActionResult> Delete(int PalestranteId)
        {
            try
            {
                var evento = await _repo.GetAllPalestranteAsync(PalestranteId, false);
                if (evento == null)
                    return NotFound();                    
                
                _repo.Delete(evento);

                if(await _repo.SaveChangeAsync())
                    return Ok();
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou"); 
            }

            return BadRequest();
        }
    }
}