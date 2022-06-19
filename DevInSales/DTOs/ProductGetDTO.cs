﻿using DevInSales.Models;
using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ProductGetDTO
    {

        public string Name { get; set; }
        public decimal Suggested_Price { get; set; }
        public string Category_Name { get; set; }

        public static explicit operator ProductGetDTO(Product product)
        {
            if (product == null)
                return null;

            return new ProductGetDTO()
            {
                Name = product.Name,
                Suggested_Price = product.Suggested_Price,
                Category_Name = product.Category.Name
            };
        }
    }
}