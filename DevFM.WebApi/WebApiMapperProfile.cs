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

            //CreateMap<PacientePostDto, Paciente>().ReverseMap();
            CreateMap<PacienteDto, Paciente>().ReverseMap();

            CreateMap<PacienteDto, Paciente>()
              .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPaciente))
              .ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesPaciente))
              .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPaciente))
              .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPaciente)).ReverseMap();

            CreateMap<PacientePostDto, Paciente>()
               .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPaciente))
               .ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesPaciente))
               .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPaciente))
               .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPaciente)).ReverseMap();




            CreateMap<CuidadorPostDto, Cuidador>().ReverseMap();
            CreateMap<TelefonePostDto, Telefone>().ReverseMap();

            CreateMap<EnderecoPostDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelPostDto, Responsavel>().ReverseMap();
            CreateMap<AtendimentoPostDto, Atendimento>().ReverseMap();


            CreateMap<EnderecoDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelDto, Responsavel>()
                .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
            CreateMap<ResponsavelPostDto, Responsavel>()
               .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
            CreateMap<AtendimentoDto, Atendimento>().ReverseMap();

        }
    }
}
