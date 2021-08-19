using System;

namespace DemoApp.Infrastructure.ExternalApis
{
    [Serializable]
    public sealed class FailedPublicDependencyException : Exception
    {
        public FailedPublicDependencyException(string? message) : base(message)
        {
        }
    }
}
