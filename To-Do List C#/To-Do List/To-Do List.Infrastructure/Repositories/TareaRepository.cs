using To_Do_List.Domain.Entities;
using To_Do_List.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List.Domain.Exceptions;
using System;

namespace To_Do_List.Infrastructure.Repositories
{
    /// <summary>
    /// Se Heredan los metodos del Repositorio Generico.
    /// Si se desea cambiar algo especifico de alguno se puede sobreescribir el metodo.
    /// Se agregan otros dos metodos especificos de la clase.
    /// </summary>
    public class TareaRepository : BaseRepository<Tarea>
    {
        public TareaRepository(DataContext context) : base(context)
        {
        }

        public async Task<Tarea> GetByName(string nombre)
        {
            return await _context.Tareas.Where(x => !x.SoftDelete).FirstOrDefaultAsync(x => x.Nombre == nombre);
        }        
        public async Task<Tarea> GetByState(bool estado)
        {
            return await _context.Tareas.Where(x => !x.SoftDelete).FirstOrDefaultAsync(x => x.Estado == estado);
        }
        public async Task<Tarea> TareaActiva(bool estado)
        {
            var activa = await GetByState(estado);
            if (estado == true)
            {
                activa.Estado = false;
            };
            activa.Estado = true;
            activa.ModificationDate = DateTime.Now;
            var element = _model.Update(activa);
            return element.Entity;
        }
    }
}
