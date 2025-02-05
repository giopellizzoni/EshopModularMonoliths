using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.MapCarter();
await app.RunAsync();
