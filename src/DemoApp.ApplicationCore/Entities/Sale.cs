using DemoApp.ApplicationCore.GeneralAbstractions;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Sale
    {
        public int Id { get; init; }
        public decimal ApplicableTax { get; init; }
        public IEnumerable<Product> ProductsSold { get; init; }
        public Sale(int id, decimal applicableTax, IEnumerable<Product> productsSold)
        {
            Validation.ThrowIfAnyAreFalse(
                () => id >= 1,
                () => applicableTax > 0,
                () => productsSold is not null && productsSold.Any());

            Id = id;
            ApplicableTax = applicableTax;
            ProductsSold = productsSold;
        }

        private Sale() { }
     }
}
