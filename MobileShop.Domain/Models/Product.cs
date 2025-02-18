﻿namespace MobileShop.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool InStock { get; set; } = true;
        public bool IsNew { get; set; } = true;

    }
}
