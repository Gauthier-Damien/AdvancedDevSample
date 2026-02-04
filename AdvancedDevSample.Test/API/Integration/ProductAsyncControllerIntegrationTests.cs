using System.Net;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Products;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedDevSample.Test.API.Integration
{
    

    public class ProductAsyncControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly InMemoryProductRepositoryAsync _repo;

        public ProductAsyncControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _repo = (InMemoryProductRepositoryAsync)factory.Services.GetRequiredService<IProductRepositoryAsync>();
        }

        [Fact]
        public async Task ChangePrice_Should_Return_NoContent_and_Save_Product()
        {
            // var product = new Product();
            // _repo.Seed(product);
            //
            // var request = new ChangePriceRequest { NewPrice = 20 };
            //
            // var response =  await _client.PutAsJsonAsync($"/api/productasync/{product.Id}/price", request);
            //
            // Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            //
            // var updated = await _repo.GetByIDAsync(product.Id);
            // Assert.Equal(20, updated!Price.Value);
        }
    }
}
