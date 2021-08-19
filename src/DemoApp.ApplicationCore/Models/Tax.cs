using DemoApp.ApplicationCore.GeneralAbstractions;

namespace DemoApp.ApplicationCore.Models
{
    public sealed record Tax
    {
        public decimal Rate { get; private init; }

        public Tax(decimal rate)
        {
            Validation.ThrowIfAnyAreFalse(() => rate is > 0 and < 100);

            Rate = rate;
        }
    }
}
