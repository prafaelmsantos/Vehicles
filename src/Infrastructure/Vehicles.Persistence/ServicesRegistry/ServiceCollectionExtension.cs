namespace Vehicles.Persistence.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleImageRepository, VehicleImageRepository>();
            services.AddScoped<IMarkRepository, MarkRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            #endregion

            #region Services
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IMarkService, MarkService>();
            services.AddScoped<IModelService, ModelService>();
            #endregion

            return services;
        }
    }
}
