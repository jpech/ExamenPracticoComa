using AutoMapper;
using Domain.Entities;
using Services.Models.Response;

namespace Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Persona, PersonaResponse>();
        }
    }
}
