using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.GeneralAbstractions;
using FluentAssertions;
using Xunit;

namespace DemoApp.ApplicationCore.Tests.GenericAbstractions
{
    public class ResultTests
    {
        private class SomeFailureObject
        {
            public string FailureMessage => "Operation failed";
        }

        [Fact]
        public void ShouldProvideResultOnSuccessfulOperation()
        {
            // given
            var validResult = new Product(1, "Test Product Name", ProductType.Micallaneous, 1.25m, "Test Manufacturer");

            // when
            var result = new Result<Product, SomeFailureObject>(validResult);

            var extractedValue = result.Extract(
                validResult => validResult,
                failure => default
                );

            // then
            extractedValue.Should().BeOfType(typeof(Product)).And.BeSameAs(validResult);
        }

        [Fact]
        public void ShouldMakeDetailsAccessibleOnFailure()
        {
            // given
            var failureResult = new SomeFailureObject();
            var expectedFailureMessage = string.Empty;

            // when
            var result = new Result<Product, SomeFailureObject>(failureResult);

            var extractedValue = result.Extract(
                validResult => validResult,
                failure => { expectedFailureMessage = failure.FailureMessage; return default; }
                );

            // then
            expectedFailureMessage.Should().BeSameAs(failureResult.FailureMessage);
        }
    }
}
