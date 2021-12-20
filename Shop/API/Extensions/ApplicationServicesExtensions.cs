using System.Linq;
using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<StoreContext>();
            // services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped((typeof(IGenericRepository<>)), (typeof(GenericRepository<>)));
            services.AddAutoMapper((typeof(MappingProfiles)));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>{
                    var errors = actionContext.ModelState.
                        Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(e => e.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse{
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddSwaggerGen(sw => 
                {
                    sw.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"});
                }
            );

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}