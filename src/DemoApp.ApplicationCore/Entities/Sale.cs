using DemoApp.ApplicationCore.GeneralAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Sale
    {
        public int Id { get; init; }
        public decimal ApplicableTax { get; init; }
        public IEnumerable<Product> ProductsSold { get; init; }
        public DateTime SoldOn { get; init; }

        public Sale(int id, decimal applicableTax, DateTime soldOn, IEnumerable<Product> productsSold)
        {
            Validation.ThrowIfAnyAreFalse(
                () => id >= 1,
                () => applicableTax > 0,
                () => productsSold is not null && productsSold.Any());

            Id = id;
            ApplicableTax = applicableTax;
            ProductsSold = productsSold;
            SoldOn = DateTime.Now;
        }

        private Sale() { }
     }
}
