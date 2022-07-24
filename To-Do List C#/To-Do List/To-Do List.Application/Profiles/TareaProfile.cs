using AutoMapper;
using To_Do_List.Application.DTOs.Tarea;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Application.Profiles
{
    public class TareaProfile : Profile
    {
        public TareaProfile()
        {
            CreateMap<Tarea, TareaResponse>();
            CreateMap<TareaRequest, Tarea>();
        }
    }
}
