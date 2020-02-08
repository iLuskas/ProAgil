using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }

        //Métodos Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

         public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Get Eventos

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(q => q.DataEvento)
                         .Where(q => q.EventoId == EventoId);
            
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(q => q.DataEvento);
            
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(q => q.DataEvento)
                         .Where(q => q.Tema.ToLower().Contains(tema.ToLower()));
            
            return await query.ToArrayAsync();
        }

        //Get Palestrantes
        public async Task<Palestrante> GetAllPalestranteAsync(int PalestrandeId, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(e => e.RedeSociais);

            if (includeEvento)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking()
                         .OrderBy(q => q.Nome)
                         .Where(q => q.Id == PalestrandeId);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(e => e.RedeSociais);

            if (includeEvento)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking()
                         .Where(q => q.Nome.ToLower().Contains(name.ToLower()));
            
            return await query.ToArrayAsync();
        }

    }
}