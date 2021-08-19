
using DemoApp.ApplicationCore.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DemoApp.PublicApi.Controllers.DTO
{
    public sealed record SaleResponseDto
    {
        public List<Product> ProductsSold { get; init; }
        public DateTime SoldOn { get; init; }
    }
}
