﻿using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; private set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public string Image { get; private set; } = string.Empty;

        public ICollection<CartProduct>? CartProducts { get; set; }
        public ICollection<SaleProduct>? SaleProducts { get; set; }

        public Product()
        {
        }

        public Product(string title,
                      decimal price,
                      string description,
                      string category,
                      string image)
        {
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
        }
    }
}
