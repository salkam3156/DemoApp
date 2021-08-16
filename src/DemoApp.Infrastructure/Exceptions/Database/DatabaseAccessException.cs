using System;

namespace DemoApp.Infrastructure.Exceptions.Database
{
    [Serializable]
    public class DatabaseAccessException : Exception
    {
        public DatabaseAccessException(string? message) : base(message)
        {
        }
    }
}
