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

            CreateMap<CuidadorDto, Cuidador>()
              .ForMember(o => o.Telefones, m => m.MapFrom(x => x.TelefonesCuidador)).ReverseMap();

            CreateMap<PacienteDto, Paciente>()              
              .ForMember(o => o.Telefones, m => m.MapFrom(x => x.TelefonesPaciente)).ReverseMap();


            CreateMap<PacienteDto, Paciente>()
              .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPaciente))
              .ForMember(o => o.Telefones, m => m.MapFrom(x => x.TelefonesPaciente))
              //.ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesPaciente))
              .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPaciente))
              .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPaciente))
              .ForMember(o => o.Paciente_Pacotes, m => m.MapFrom(x => x.Paciente_Pacotes)).ReverseMap();

            CreateMap<PacientePostDto, Paciente>()
               .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPacientePostDtos))
               .ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesPacientePostDtos))
               .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPacientePostDtos))
               .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPacientePostDtos))
               .ForMember(o => o.Paciente_Pacotes, m => m.MapFrom(x => x.Paciente_PacotePostPostDtos)).ReverseMap();

            CreateMap<PacientePutDto, Paciente>()
                .ForMember(o => o.RespensaveisPacientes, m => m.MapFrom(x => x.ResponsaveisPacientePutDtos))
                .ForMember(o => o.TelefonesPacientes, m => m.MapFrom(x => x.TelefonesCuidadorPutDtos))
                .ForMember(o => o.AtendimentosPacientes, m => m.MapFrom(x => x.AtendimentosPacientePutDtos))
                .ForMember(o => o.EnderecosPaciente, m => m.MapFrom(x => x.EnderecosPacientePutDtos))
                .ForMember(o => o.Paciente_Pacotes, m => m.MapFrom(x => x.Paciente_PacotePutDtos)).ReverseMap();

            CreateMap<PacoteDto, Pacote>().ReverseMap();
            CreateMap<PacotePostDto, Pacote>().ReverseMap();            
            CreateMap<PacoteUpdateDto, Pacote>().ReverseMap();
            CreateMap<Paciente_PacoteDto, Paciente_Pacote>().ReverseMap();
            CreateMap<Paciente_PacotePostDto, Paciente_Pacote>().ReverseMap();
            CreateMap<Paciente_PacotePutDto, Paciente_Pacote>().ReverseMap();



            CreateMap<CuidadorPostDto, Cuidador>().ReverseMap();
            CreateMap<TelefonePostDto, Telefone>().ReverseMap();
            

            CreateMap<CuidadorPutDto, Cuidador>().ReverseMap();
            CreateMap<TelefonePutDto, Telefone>().ReverseMap();

            CreateMap<EnderecoPostDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelPostDto, Responsavel>().ReverseMap();
            CreateMap<AtendimentoPostDto, Atendimento>().ReverseMap();

            CreateMap<EnderecoPutDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelPutDto, Responsavel>().ReverseMap();
            CreateMap<AtendimentoPutDto, Atendimento>().ReverseMap();


            CreateMap<EnderecoDto, Endereco>().ReverseMap();
            CreateMap<ResponsavelDto, Responsavel>()
                .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
            CreateMap<ResponsavelPostDto, Responsavel>()
               .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
            CreateMap<AtendimentoDto, Atendimento>()
                 .ForMember(destino => destino.DescricaoTurno, opt => opt.MapFrom(origem => origem.Turno))
                 .ForMember(o => o.TelefonesCuidador, m => m.MapFrom(x => x.TelefonesCuidador)).ReverseMap();
            CreateMap<ResponsavelPutDto, Responsavel>()
                 .ForMember(o => o.TelefonesResponsaveis, m => m.MapFrom(x => x.TelefonesResponsavel)).ReverseMap();
         

            CreateMap<UsuarioPostDto, Usuario>().ReverseMap();
            CreateMap<UsuarioPutDto, Usuario>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<PerfilDto, Perfil>().ReverseMap();
            CreateMap<PerfilPostDto, Perfil>().ReverseMap();

        }
    }
}

