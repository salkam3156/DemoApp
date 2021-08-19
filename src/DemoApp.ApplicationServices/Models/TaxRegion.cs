using DemoApp.ApplicationCore.GeneralAbstractions;

namespace DemoApp.ApplicationServices.Models
{
    public sealed record TaxRegion
    {
        public string Region { get; private init; }   
        public TaxRegion(string region) 
        {
            Validation.ThrowIfAnyAreFalse(() => string.IsNullOrWhiteSpace(region) is false);

            Region = region;
        }
    }
}
