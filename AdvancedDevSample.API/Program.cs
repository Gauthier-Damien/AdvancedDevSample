using AdvancedDevSample.API.Middlewares;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Domain.Interfaces.Suppliers;
using AdvancedDevSample.Domain.Interfaces.Users;
using AdvancedDevSample.Domain.Interfaces.Orders;
using AdvancedDevSample.Infrastructure.Repositories;

// Point d'entrée principal de l'application ASP.NET Core.
// Configure l'injection de dépendances, les services et le pipeline HTTP.
var builder = WebApplication.CreateBuilder(args);

// Configuration des contrôleurs API
builder.Services.AddControllers();

// Configuration de Swagger pour la documentation de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Inclusion automatique des commentaires XML de tous les assemblies dans Swagger
    var basepath = AppContext.BaseDirectory;
    var xmlfiles = Directory.GetFiles(basepath, "*.xml");
    foreach (var xmlfile in xmlfiles)
    {
        options.IncludeXmlComments(xmlfile);
    }
});

// Enregistrement des services applicatifs avec cycle de vie Scoped (une instance par requête HTTP)
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();

// Enregistrement des repositories d'infrastructure (pattern Repository)
// Couplage faible : interfaces du Domain, implémentations dans Infrastructure
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ISupplierRepository, EfSupplierRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

var app = builder.Build();

// Configuration du pipeline HTTP : ordre d'exécution des middlewares
if (app.Environment.IsDevelopment())
{
    // Swagger uniquement en développement pour des raisons de sécurité
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirection automatique HTTP vers HTTPS
app.UseHttpsRedirection();

// Middleware d'autorisation (même si non implémenté, conservé pour évolution future)
app.UseAuthorization();

// Enregistrement des routes des contrôleurs
app.MapControllers();

// Démarrage de l'application
await app.RunAsync();
