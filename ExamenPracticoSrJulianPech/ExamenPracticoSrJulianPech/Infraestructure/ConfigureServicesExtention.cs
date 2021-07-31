using Microsoft.Extensions.DependencyInjection;
using Services.Implementaciones;
using Services.Interfaces;

namespace ExamenPracticoSrJulianPech.Infraestructure
{
    public static class ConfigureServicesExtention
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonaService, PersonaService>();
        }
    }
}
