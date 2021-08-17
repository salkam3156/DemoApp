using DemoApp.ApplicationCore.Exceptions;
using DemoApp.ApplicationCore.GeneralAbstractions;
using FluentAssertions;
using System;
using Xunit;

namespace DemoApp.ApplicationCore.Tests.GenericAbstractions
{
    public class ValidationTests
    {
        [Fact]
        public void ShouldThrowIfAnyAreFalse()
        {
            Action validationAction = ()
                => Validation.ThrowIfAnyAreFalse(
                    () => 1 == 1,
                    () => 1 == 0);

            validationAction
                .Should()
                .ThrowExactly<ObjectCreationException>()
                .WithMessage("Validation failed while creating a domain object.");
        }

        [Fact]
        public void ShouldNotThrowIfAllTrue()
        {
            Action validationAction = ()
                => Validation.ThrowIfAnyAreFalse(
                    () => true is true,
                    () => true,
                    () => 1 == 1);

            validationAction.Should().NotThrow();
        }
    }
}
