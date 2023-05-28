using AutoMapper;
using DevFM.Domain.Models;
using DevFM.WebApi.Dtos;
using System;

namespace DevFM.WebApi
{
    public class WebApiMapperProfile : Profile
    {
        /// <inheritdoc />
        public WebApiMapperProfile()
        {
           
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
            CreateMap<CuidadorDto, Cuidador>().ReverseMap();
            CreateMap<TelefoneDto, Telefone>().ReverseMap();
            CreateMap<EstadoDto, Estado>().ReverseMap();
            CreateMap<EstadoCivilDto, EstadoCivil>().ReverseMap();
            CreateMap<MunicipioDto, Municipio>().ReverseMap();
            CreateMap<TipoTelefoneDto, TipoTelefone>().ReverseMap();
            CreateMap<TurnoDto, Turno>().ReverseMap();


            CreateMap<CuidadorPostDto, Cuidador>().ReverseMap();
            CreateMap<TelefonePostDto, Telefone>().ReverseMap();

        }
    }
}
