public static class JsonSupportDependencyInjection {
    public static IServiceCollection AddJsonSupport(this IServiceCollection services) {
        services.AddControllers().AddNewtonsoftJson(options => {
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;// Exemplo: formatar JSON com indentação
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Exemplo: ignorar valores nulos
        });

        return services;
    }
}