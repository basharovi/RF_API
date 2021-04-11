using System;
using RapidFireLib.Lib.Core;
using System.ComponentModel.DataAnnotations;

namespace BusinessDomain.Aggregates.Product
{
    public class ProductInfo : IModel
    {
        [Key]
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
