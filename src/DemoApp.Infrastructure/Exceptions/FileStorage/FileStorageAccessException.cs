using System;

namespace DemoApp.Infrastructure.Exceptions.FileStorage
{
    [Serializable]
    public sealed class FileStorageAccessException : Exception
    {
        public FileStorageAccessException(string? message) : base(message)
        {
        }
    }
}
