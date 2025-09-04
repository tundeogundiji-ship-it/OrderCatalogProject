﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Dtos.Products
{
    public class CreateProductDto : IProductDto
    {
        public string? Name { get ; set ; }
        public string? Description { get ; set ; }
        public decimal Price { get ; set ; }
        public int StockQuantity { get ; set ; }
    }
}
