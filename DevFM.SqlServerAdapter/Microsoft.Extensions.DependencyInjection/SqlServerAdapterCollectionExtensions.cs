using DevFM.Domain.Adapters;
using DevFM.SqlServerAdapter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerAdapterCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddSqlServerAdapter(
            this IServiceCollection services,
            SqlServerAdapterConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            
            services.AddSingleton(configuration);
            services.AddScoped(d => new SqlAdapterContext(configuration.SqlConnectionString));

            services.AddScoped<ICategoriaSqlReadAdapter, CategoriaSqlReadAdapter>();
            services.AddScoped<IEstadoCivilSqlReadAdapter, EstadoCivilSqlReadAdapter>();
            services.AddScoped<IEstadoSqlReadAdapter, EstadoSqlReadAdapter>();
            services.AddScoped<IMunicipioSqlReadAdapter, MunicipioSqlReadAdapter>();
            services.AddScoped<ITipoTelefoneSqlReadAdapter, TipoTelefoneSqlReadAdapter>();
            services.AddScoped<ITurnoSqlReadAdapter, TurnoSqlReadAdapter>();
            services.AddScoped<ICuidadorSqlReadAdapter, CuidadorSqlReadAdapter>();
            services.AddScoped<IPacienteSqlReadAdapter, PacienteSqlReadAdapter>();
            services.AddScoped<IPacoteSqlReadAdapter, PacoteSqlReadAdapter>();
            services.AddScoped<IUsuarioSqlReadAdapter, UsuarioSqlReadAdapter>();

            return services;
        }
    }
}
