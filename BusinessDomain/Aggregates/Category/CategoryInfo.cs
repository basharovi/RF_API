using BusinessDomain.Aggregates.Product;
using RapidFireLib.Lib.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessDomain.Aggregates.Category
{
    public class CategoryInfo : IModel
    {
        public CategoryInfo()
        {
            ProductInfo = new List<ProductInfo>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductInfo> ProductInfo { get; set; }
    }
}
