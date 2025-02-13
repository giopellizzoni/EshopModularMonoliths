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

builder.Services.AddMassTranssitWithAssemblies(catalogAssembly, basketAssembly, orderingAssembly);

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

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

await app.RunAsync();
