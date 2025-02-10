var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDatabaseDependencies(builder.Configuration).AddControllers();
builder.Services.AddJsonSupportDependencyInjection();
builder.Services.AddAuthenticationDependencyInjection(builder.Configuration);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5029); // Porta HTTP
    serverOptions.ListenAnyIP(7070, listenOptions =>
    {
        listenOptions.UseHttps(); // Porta HTTPS com certificado de desenvolvimento
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();