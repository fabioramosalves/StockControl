using Microsoft.Extensions.DependencyInjection;

namespace StockControl.Common
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            return services;
        }
    }
}
