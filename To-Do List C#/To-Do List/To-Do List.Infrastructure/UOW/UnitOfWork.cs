using To_Do_List.Infrastructure.Data;
using To_Do_List.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace To_Do_List.Infrastructure.UOW
{
    /// <summary>
    /// Se implementa el patron UOW asi todo pasa por un solo lugar antes de ir a la DB.
    /// </summary>
    public class UnitOfWork
    {
        private readonly DataContext _context;

        #region Repositories Declaration

        protected TareaRepository _tareaRepository;

        #endregion
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #region Repositories Implementation
        public TareaRepository TareaRepository
        {
            get
            {
                if (_tareaRepository == null)
                {
                    _tareaRepository = new TareaRepository(_context);
                }
                return _tareaRepository;
            }
        }
        #endregion
    }
}
