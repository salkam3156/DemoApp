using DemoApp.ApplicationCore.GeneralAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Sale
    {
        public int Id { get; private init; }
        public decimal ApplicableTax { get; private init; }
        public IEnumerable<Product> ProductsSold { get; private init; }
        public DateTime SoldOn { get; private init; }

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
