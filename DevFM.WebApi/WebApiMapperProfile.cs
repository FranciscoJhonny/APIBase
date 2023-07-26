using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.ViewModels;
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
            CreateMap<TelefoneDto, TelefoneVM>().ReverseMap();
            //CreateMap<PacientePostDto, Paciente>().ReverseMap();
            CreateMap<PacienteDto, Paciente>().ReverseMap();

            CreateMap<CuidadorDto, Cuidador>()
              .ForMember(o => o.TelefonesCuidador, m => m.MapFrom(x => x.TelefonesCuidador)).ReverseMap();

            CreateMap<PacienteDto, Paciente>()
              .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPacienteDtos))
              .ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesPacienteDtos))
              .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPacienteDtos))
              .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPacienteDtos))
              .ForMember(o => o.Paciente_Pacotes, m => m.MapFrom(x => x.Paciente_PacoteDtos)).ReverseMap();

            CreateMap<PacientePostDto, Paciente>()
               .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPacientePostDtos))
               .ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesPacientePostDtos))
               .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPacientePostDtos))
               .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPacientePostDtos))
               .ForMember(o => o.Paciente_Pacotes, m => m.MapFrom(x => x.Paciente_PacotePostPostDtos)).ReverseMap();
            CreateMap<PacoteDto, Pacote>().ReverseMap();
            CreateMap<PacotePostDto, Pacote>().ReverseMap();
            CreateMap<PacoteUpdateDto, Pacote>().ReverseMap();
            CreateMap<Paciente_PacoteDto, Paciente_Pacote>().ReverseMap();
            CreateMap<Paciente_PacotePostDto, Paciente_Pacote>().ReverseMap();



            CreateMap<CuidadorPostDto, Cuidador>().ReverseMap();
            CreateMap<TelefonePostDto, Telefone>().ReverseMap();

            CreateMap<CuidadorPutDto, Cuidador>().ReverseMap();
            CreateMap<TelefonePutDto, Telefone>().ReverseMap();

            CreateMap<EnderecoPostDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelPostDto, Responsavel>().ReverseMap();
            CreateMap<AtendimentoPostDto, Atendimento>().ReverseMap();


            CreateMap<EnderecoDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelDto, Responsavel>()
                .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
            CreateMap<ResponsavelPostDto, Responsavel>()
               .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
            CreateMap<AtendimentoDto, Atendimento>().ReverseMap();

            CreateMap<UsuarioPostDto, Usuario>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<PerfilDto, Perfil>().ReverseMap();
            CreateMap<PerfilPostDto, Perfil>().ReverseMap();

        }
    }
}

