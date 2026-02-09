using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs
{
    

    public class ChangePriceRequest
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
