using DevFM.Application.Services;
using DevFM.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevFM.Application.Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddApplicationService(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICuidadorService, CuidadorService>();
            services.AddScoped<IEstadoCivilService, EstadoCivilService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IMunicipioService, MunicipioService>();
            services.AddScoped<ITipoTelefoneService, TipoTelefoneService>();
            services.AddScoped<ITurnoService, TurnoService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IPacoteService, PacoteService>();
            services.AddScoped<IUsuarioService, UsuarioService>();


            return services;
        }
    }
}
