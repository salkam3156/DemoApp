using DemoApp.ApplicationCore.GeneralAbstractions;

namespace DemoApp.ApplicationCore.Models
{
    public sealed record Notification
    {
        public string Message { get; private init; }
        
        public Notification(string message)
        {
            Validation.ThrowIfAnyAreFalse(
                () => string.IsNullOrWhiteSpace(message) is false);

            message = message;
        }
    }
}
