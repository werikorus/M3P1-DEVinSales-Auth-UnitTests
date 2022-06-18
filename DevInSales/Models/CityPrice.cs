﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DevInSales.Models
{    
    [ExcludeFromCodeCoverage]
    public class CityPrice
    {
        [Key]
        public int Id { get; set; }     
        public int CityId { get; set; }
        public City City { get; set; }        
        public ShippingCompany ShippingCompany { get; set; }
        public decimal BasePrice { get; set; }
        public int ShippingCompanyId { get; set; }
    }
}
