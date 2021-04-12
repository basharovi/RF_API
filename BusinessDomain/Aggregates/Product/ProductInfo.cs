using RapidFireLib.Lib.Core;
using System.ComponentModel.DataAnnotations;

namespace BusinessDomain.Aggregates.Product
{
    public class ProductInfo : IModel
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
