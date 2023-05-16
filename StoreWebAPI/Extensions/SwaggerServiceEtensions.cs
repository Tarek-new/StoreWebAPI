using Microsoft.OpenApi.Models;
using System.Reflection;

namespace StoreWebAPI.Extensions
{
    public static class SwaggerServiceEtensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Store API", Version = "v1" });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Auth Bearer Scheme",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }

                };
                options.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securitySchema,new string[]{ } }
                };
                options.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
    }
}
