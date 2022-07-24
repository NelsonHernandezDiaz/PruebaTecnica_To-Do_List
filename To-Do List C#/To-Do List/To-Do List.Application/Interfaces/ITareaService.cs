using To_Do_List.Application.Common;
using To_Do_List.Application.DTOs.Tarea;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace To_Do_List.Application.Interfaces
{
    /// <summary>
    /// Interfaz sobre la que aplicar los métodos de extensión a crearse para todos los servicios .
    /// Los servicios que la implementan tendrán acceso a dichos métodos.
    /// </summary>
    public interface ITareaService
    {
        Task<IEnumerable<TareaResponse>> GetAll();
        Task<TareaResponse> GetById(int id);
        Task<TareaResponse> GetByName(string nombre);        
        Task<TareaResponse> GetByState(bool estado);
        Task<Result> Insert(TareaRequest request);
        Task<Result> Update(int id, TareaRequest request);
        Task<Result> Delete(int id);
        Task<Result> TareaActiva(bool estado);
    }
}
