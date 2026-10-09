using AutoMapper;
using Remis.API.DTOs.Persona;
using Remis.CORE.Entidades;

namespace Remis.API.AutoMapper
{
    public class MappeoObjeto : Profile
    {
        public MappeoObjeto()
        {
           CreateMap<Persona, PersonaDto>();
            CreateMap<PersonaCrearDto, Persona>();
           CreateMap<PersonaActualizarDto, Persona>(); 
            
        }
    }
}



//CreateMap<Remis.CORE.Entidades.Persona, Remis.API.DTOs.Persona.PersonaDto>();
//CreateMap<Remis.API.DTOs.Persona.PersonaCrearDto, Remis.CORE.Entidades.Persona>();
//CreateMap<Remis.API.DTOs.Persona.PersonaActualizarDto, Remis.CORE.Entidades.Persona>();