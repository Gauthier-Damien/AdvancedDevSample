using AdvancedDevSample.API.Middlewares;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Domain.Interfaces.Suppliers;
using AdvancedDevSample.Domain.Interfaces.Users;
using AdvancedDevSample.Domain.Interfaces.Orders;
using AdvancedDevSample.Domain.Interfaces.Auth;
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
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ISupplierRepository, EfSupplierRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

var app = builder.Build();

// Seed des utilisateurs de démo en développement
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var authRepo = scope.ServiceProvider.GetRequiredService<IAuthRepository>();
    
    authRepo.SeedUser("demo", "demo123", "Student");
    authRepo.SeedUser("admin", "admin123", "Admin");
    
    Console.WriteLine("Comptes de demo crees automatiquement:");
    Console.WriteLine("   Student: demo / demo123");
    Console.WriteLine("   Admin: admin / admin123");
}

// Enregistrement du middleware de gestion centralisée des exceptions
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configuration du pipeline HTTP : ordre d'exécution des middlewares
if (app.Environment.IsDevelopment())
{
    // Swagger uniquement en développement pour des raisons de sécurité
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdvancedDevSample API v1");
        c.DocumentTitle = "AdvancedDevSample API - Documentation";
    });
}

// Redirection automatique HTTP vers HTTPS
app.UseHttpsRedirection();

// Authentification JWT (doit être avant UseAuthorization)
app.UseAuthentication();

// Middleware d'autorisation
app.UseAuthorization();

// Enregistrement des routes des contrôleurs
app.MapControllers();

// Démarrage de l'application
await app.RunAsync();
