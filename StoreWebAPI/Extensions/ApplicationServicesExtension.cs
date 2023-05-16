using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Helpers;
using StoreWebAPI.ResponseStatusModules;

namespace StoreWebAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductBrandsRepository, ProductBrandsRepository>();
            services.AddScoped<IProductTypesRepository, ProductTypesRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));



            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = actions =>
                {
                    var error = actions.ModelState
                                     .Where(e => e.Value.Errors.Count > 0)
                                     .SelectMany(e => e.Value.Errors)
                                     .Select(e => e.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidaionErrorResponse
                    {
                        Errors = error
                    };

                    return new BadRequestObjectResult(errorResponse);
                };


            });


            return services;

        }
    }
}
