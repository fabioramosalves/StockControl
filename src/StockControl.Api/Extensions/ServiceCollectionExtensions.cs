using MediatR;
using StockControl.Application.DataBase.ProductStock.Handlers;
using StockControl.Application.Handles.Notifications;
using StockControl.Domain.Repositories;
using StockControl.Persistence.Repositories.Producto;
using System.Reflection;

namespace StockControl.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CreateProductHandler).Assembly);
            services.AddMediatR(typeof(UpdateProductHandler).Assembly);
            services.AddMediatR(typeof(DeleteProductHandler).Assembly);
            services.AddMediatR(typeof(GetProductByIdHandler).Assembly);
            services.AddMediatR(typeof(AddStockCommandHandler).Assembly);
            services.AddMediatR(typeof(LogErrorHandler).Assembly);

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
