using DemoApp.ApplicationCore.GeneralAbstractions;

namespace DemoApp.ApplicationServices.Models
{
    public sealed record Notification
    {
        public string Message { get; private init; }
        
        public Notification(string message)
        {
            Validation.ThrowIfAnyAreFalse(
                () => string.IsNullOrWhiteSpace(message) is false);

            Message = message;
        }
    }
}
