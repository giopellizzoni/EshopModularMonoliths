using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

// logging
builder.Host.UseSerilog(
    (
        context,
        config) => config.ReadFrom.Configuration(context.Configuration));

// common services
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var orderingAssembly = typeof(OrderingModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(catalogAssembly, basketAssembly);

builder.Services
    .AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddStackExchangeRedisCache(
    options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });

builder.Services.AddMassTranssitWithAssemblies(builder.Configuration, catalogAssembly, basketAssembly, orderingAssembly);
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// module services
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => {});
app.UseAuthentication();
app.UseAuthorization();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

await app.RunAsync();
