using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;

        //Eventos
         Task<bool> SaveChangeAsync();
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
         Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);

        //Palestrantes
        Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEventos);
        Task<Palestrante> GetAllPalestranteAsync(int PalestrandeId, bool includeEventos);

    }
}