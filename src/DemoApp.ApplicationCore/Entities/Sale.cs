using DemoApp.ApplicationCore.GeneralAbstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Sale
    {
        private readonly ILazyLoader _lazyloader;

        public int Id { get; private init; }
        public decimal ApplicableTax { get; private init; }

        private List<Product> _productsSold;
        public List<Product> ProductsSold
        {
            get => _lazyloader.Load(this, ref _productsSold);
            private set => _productsSold = value;
        }
        public DateTime SoldOn { get; private init; }

        public Sale(int id, decimal applicableTax, IEnumerable<Product> productsSold)
        {
            Validation.ThrowIfAnyAreFalse(
                () => id >= 1,
                () => applicableTax > 0,
                () => productsSold is not null && productsSold.Any());

            Id = id;
            ApplicableTax = applicableTax;
            ProductsSold = productsSold.ToList();
            SoldOn = DateTime.Now;
        }

        public Sale(decimal applicableTax, DateTime soldOn, IEnumerable<Product> productsSold)
        {
            Validation.ThrowIfAnyAreFalse(
                () => applicableTax > 0,
                () => productsSold is not null && productsSold.Any());

            ApplicableTax = applicableTax;
            ProductsSold = productsSold.ToList();
            SoldOn = soldOn;
        }

        private Sale() { }
        private Sale(ILazyLoader lazyloader) => _lazyloader = lazyloader;
     }
}
