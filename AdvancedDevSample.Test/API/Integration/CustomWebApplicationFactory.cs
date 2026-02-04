using AdvancedDevSample.Domain.Interfaces.Products;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AdvancedDevSample.Test.API.Integration
{
    
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {   
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
               services.RemoveAll(typeof(IProductRepositoryAsync));
               services.AddSingleton<IProductRepositoryAsync, InMemoryProductRepositoryAsync>();
            });
        }
    }
}
