using AutoMapper;
using To_Do_List.Application.Common;
using To_Do_List.Application.DTOs.Tarea;
using To_Do_List.Application.Interfaces;
using To_Do_List.Domain.Entities;
using To_Do_List.Infrastructure.UOW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace To_Do_List.Application.Services
{
    /// <summary>
    /// Todos los Metodos que se utilizaran y la logica de negocio implementada.
    /// </summary>
    public class TareaService : ITareaService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TareaService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Public Methods
        public async Task<IEnumerable<TareaResponse>> GetAll()
        {
            var result = await _unitOfWork.TareaRepository.GetAll();
            return _mapper.Map<IEnumerable<TareaResponse>>(result);
        }
        public async Task<TareaResponse> GetById(int id)
        {
            var result = await _unitOfWork.TareaRepository.GetById(id);
            return _mapper.Map<TareaResponse>(result);
        }
        public async Task<TareaResponse> GetByName(string nombre)
        {
            var result = await _unitOfWork.TareaRepository.GetByName(nombre);
            return _mapper.Map<TareaResponse>(result);
        }
        public async Task<TareaResponse> GetByState(bool estado)
        {
            var result = await _unitOfWork.TareaRepository.GetByState(estado);
            return _mapper.Map<TareaResponse>(result);
        }
        public async Task<Result> Insert(TareaRequest request)
        {
            var username = await _unitOfWork.TareaRepository.GetByName(request.Nombre);
            if (username != null)
            {
                return new Result().Fail("Ya Existe un Registro de esta");
            }
            else
            {
                var entity = _mapper.Map<Tarea>(request);

                await _unitOfWork.TareaRepository.Insert(entity);
                await _unitOfWork.Save();
                return new Result().Success($"Se ha agregado la Tarea correctamente al sistema");
            }
        }
        public async Task<Result> Update(int id, TareaRequest request)
        {
            var update = await _unitOfWork.TareaRepository.GetById(id);
            if (update == null)
            {
                return new Result().NotFound();
            }
            else
            {
                var entity = _mapper.Map<Tarea>(request);

                await _unitOfWork.TareaRepository.Update(entity.Id, entity);
                await _unitOfWork.Save();
                return new Result().Success($"Se han aplicado los cambios correctamente en el sistema");
            }
        }
        public async Task<Result> Delete(int id)
        {
            var delete = await _unitOfWork.TareaRepository.GetById(id);
            if (delete == null)
            {
                return new Result().NotFound();
            }
            else
            {
                var entity = _mapper.Map<Tarea>(delete);

                await _unitOfWork.TareaRepository.Delete(entity.Id);
                await _unitOfWork.Save();
                return new Result().Success($"Se ha eliminado la Tarea del sistema");
            }
        }
        public async Task<Result> TareaActiva(bool estado)
        {
            var status = await _unitOfWork.TareaRepository.GetByState(estado);
            if (status == null)
            {
                return new Result().NotFound();
            }
            else
            {
                var entity = _mapper.Map<Tarea>(estado);

                await _unitOfWork.TareaRepository.TareaActiva(entity.Estado);
                await _unitOfWork.Save();
                return new Result().Success($"Se ha Modificado el Estado de la Tarea en el sistema");
            }
        }
        #endregion
    }
}
