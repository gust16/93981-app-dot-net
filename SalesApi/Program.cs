using Scalar.AspNetCore;
using SalesApi.Repositories;
using SalesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ── MVC ───────────────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── OpenAPI / Swagger ─────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title       = "SalesApi",
        Version     = "v1",
        Description = "REST API for managing sales products."
    });
});

// ── Dependency Injection ──────────────────────────────────────────────────────
builder.Services.AddSingleton<IProductRepository, ProductRepository>(); // Singleton → el store en memoria persiste entre requests
builder.Services.AddScoped<IProductService,    ProductService>();

var app = builder.Build();

// ── Middleware ────────────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json"; // http://localhost:5139/openapi/v1.json
    });

    app.MapScalarApiReference(options =>                       // http://localhost:5139/scalar/v1
    {
        options.Title  = "SalesApi";
        options.Theme  = ScalarTheme.DeepSpace;
        options.OpenApiRoutePattern = "/openapi/{documentName}.json";
    });
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
