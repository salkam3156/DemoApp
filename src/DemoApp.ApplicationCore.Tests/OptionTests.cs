using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.GeneralAbstractions;
using FluentAssertions;
using Xunit;

namespace DemoApp.ApplicationCore.Tests
{

    namespace DemoApp.ApplicationCore.Tests.GenericAbstractions
    {
        public class OptionTests
        {
            private class SomeFailureObject
            {
                public string FailureMessage => "Operation failed";
            }

            [Fact]
            public void ShouldProvideResultOnSuccessfulOperation()
            {
                // given
                var validResult = new Product(1, "TestProduct", 1.25f, "This is a test product");

                // when
                var resultOption = new Option<Product, SomeFailureObject>(validResult);

                var result = resultOption.Extract(
                    validResult => validResult,
                    failure => default
                    );

                // then
                result.Should().BeOfType(typeof(Product)).And.BeSameAs(validResult);
            }

            [Fact]
            public void ShouldMakeDetailsAccessibleOnFailure()
            {
                // given
                var failureResult = new SomeFailureObject();
                var failureMessage = string.Empty;

                // when
                var resultOption = new Option<Product, SomeFailureObject>(failureResult);

                dynamic result = resultOption.Extract(
                    validResult => validResult,
                    failure => { failureMessage = failure.FailureMessage; return default; }
                    );

                // then
                failureMessage.Should().BeSameAs(failureResult.FailureMessage);

            }
        }
    }

}
