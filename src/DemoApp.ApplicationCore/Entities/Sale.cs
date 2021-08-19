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

        public List<Product> ProductsSold { get; private set; }
        public DateTime SoldOn { get; private init; }

        public Sale(decimal applicableTax, IEnumerable<Product> productsSold)
        {
            Validation.ThrowIfAnyAreFalse(
                () => applicableTax > 0,
                () => productsSold is not null && productsSold.Any());

            ApplicableTax = applicableTax;
            ProductsSold = productsSold.ToList();
            SoldOn = DateTime.Now;
        }

        private Sale() { }
     }
}
