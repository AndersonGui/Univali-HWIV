using HW.Database.Context;
using HW.Database.Repositories;
using HW.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Configurations
{
    public static class IoC
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ResolveDataDependencies(services, configuration);

            ResolveRepositoriesDependencies(services);

            return services;
        }

        private static void ResolveDataDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HWContext>(options =>
            {
                /* 
                    Descomentar para exibir o SQL gerado pelo EF no console.
                 */
                //options.UseLoggerFactory(MyLoggerFactory);
                options.UseMySql(configuration.GetConnectionString("HWMysql"));
            });
            services.AddScoped<HWContext>();
            services.AddScoped<DbContext, HWContext>();
        }

        private static void ResolveRepositoriesDependencies(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
