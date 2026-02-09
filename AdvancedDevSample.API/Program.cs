using AdvancedDevSample.API.Middlewares;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basepath = AppContext.BaseDirectory;
    var xmlfiles = Directory.GetFiles(basepath, "*.xml");
    foreach (var xmlfile in xmlfiles)
    {
        options.IncludeXmlComments(xmlfile);
    }
});

// Application dépendencies
builder.Services.AddScoped<ProductService>();


//Infrastructure dépendencies
builder.Services.AddScoped<IProductRepository, EfProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Use Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
