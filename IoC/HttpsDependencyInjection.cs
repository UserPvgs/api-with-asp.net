public static class HttpsDependencyInjection {
    public static WebApplicationBuilder AddHttpsDependencyInjection(this WebApplicationBuilder builder){
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(5029); // Porta HTTP
            serverOptions.ListenAnyIP(7070, listenOptions =>
            {
                listenOptions.UseHttps(); // Porta HTTPS com certificado de desenvolvimento
            });
        });
        return builder;
    }
}