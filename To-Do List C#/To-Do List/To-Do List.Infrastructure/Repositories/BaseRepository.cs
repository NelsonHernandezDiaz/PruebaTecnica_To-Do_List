using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Exceptions;
using To_Do_List.Infrastructure.Data;
using To_Do_List.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace To_Do_List.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio Base sobre el que se aplican todos los métodos.
    /// Se hereda de la interfase todos los metodos que se implementan.
    /// Tambien al ser Generico se puede heredar para varios repositios.
    /// Se implementan los metodos de modo Virtual en caso de desear hacer un cambio particular se pueda sobreescribir.
    /// </summary>
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _model;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _model = context.Set<T>();
        }

        #region Public Methods
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _model.Where(x => !x.SoftDelete).ToListAsync();
        }
        public virtual async Task<T> GetById(int id)
        {
            return await _model.Where(x => !x.SoftDelete).FirstOrDefaultAsync(x => x.Id == id);
        }
        public virtual async Task<T> Insert(T entity)
        {
            var element = await _model.AddAsync(entity);
            return element.Entity;
        }
        public virtual async Task<T> Update(int id, T entity)
        {
            var exist = await GetById(id);
            if (exist == null)
            {
                throw new RecordNotFoundException();
            };
            exist.ModificationDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            var element = _model.Update(exist);
            return element.Entity;
        }
        public virtual async Task<T> Delete(int id)
        {
            var exist = await GetById(id);
            if (exist == null)
            {
                throw new RecordNotFoundException();
            };
            exist.SoftDelete = true;
            exist.DeleteDate = DateTime.Now;
            var element = _model.Update(exist);
            return element.Entity;
        }
        #endregion
    }
}
