using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDevSample.API.Controllers
{
    

    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        
        public ProductController(ProductService _productService)
        {
            _productService = _productService;
        }

        [HttpPut("{id}/price")]
        public IActionResult UpdatePrice(Guid id, [FromBody] ChangePriceRequest request)
        {
            try
            {
                _productService.ChangeProductPrice(id, request.Price);
                return NoContent(); //204
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
                
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
