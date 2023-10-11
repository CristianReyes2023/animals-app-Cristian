using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Cita,CitaDto>().ReverseMap(); //Tome la entidad Pais y mappeela de acuerdo a la estructura del DTO esto permite de entidad a DTO y de DTO a entidad
        CreateMap<Ciudad,CiudadDto>().ReverseMap();
        CreateMap<Cliente,ClienteDto>().ReverseMap();
        CreateMap<ClienteTelefono,ClienteTelefonoDto>().ReverseMap();
        CreateMap<Departamento,DepartamentoDto>().ReverseMap();
        CreateMap<Mascota,MascotaDto>().ReverseMap();
        CreateMap<Pais,PaisDto>().ReverseMap();
        CreateMap<Raza,RazaDto>().ReverseMap();
        CreateMap<Servicio,ServicioDto>().ReverseMap();
    }
}

/*
Después de personalizar el DTO, tengo que establecer ese mappeo y es decirle al auto mapper que coja la clase A y la mappee con la clase DTO o viceversa
Se define una nueva clase dentro de la carpeta que se llama profiles en API, MappingProfiles debe heredar de la clase profile

Instruccion CreateMap : nos permite crear el mapeado de los objetos  y debe ir dentro del constructor
*/

