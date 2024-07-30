namespace Vehicles.GraphQL.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            #region Services
            services
                .AddGraphQLServer()
                .AddApolloTracing(HotChocolate.Execution.Options.TracingPreference.Always)
                .AddType<MarkType>()
                .AddType<ModelType>()
                .AddType<VehicleType>()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddSorting()
                .SetPagingOptions(new PagingOptions
                {
                    MaxPageSize = int.MaxValue,
                    IncludeTotalCount = true,
                    DefaultPageSize = 10
                })
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);
            #endregion

            return services;
        }
    }
}
