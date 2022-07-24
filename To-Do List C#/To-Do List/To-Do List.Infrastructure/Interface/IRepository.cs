using System.Collections.Generic;
using System.Threading.Tasks;

namespace To_Do_List.Infrastructure.Interface
{
    /// <summary>
    /// Interfaz sobre la que aplicar los métodos de extensión a crearse para todos los repositorios.
    /// Los repositorios que la implementan tendrán acceso a dichos métodos.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Delete(int id);
    }
}
