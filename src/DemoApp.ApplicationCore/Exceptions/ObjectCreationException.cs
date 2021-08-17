using System;

namespace DemoApp.ApplicationCore.Exceptions
{
    [Serializable]
    public sealed class ObjectCreationException : Exception
    {
        // Basically the only exception type that the application core itself should throw.
        // Core object should not need to be called .IsValid() or some such on by someother piece of code.
        // Someone could skip the code path, and not call IsValid or run some validation logic on it.
        // It should not live long enough for anything to use it (meaning - it should not come into existence at all).
        // Otherwise, this would carry the risk of application and data being in an inconsistent state.
        // This would be a dangerous, silent failure of the application.
        // The object should throw an exception from the constructur to not be left in some odd zombie state.
        // See this for a more general reasoning https://isocpp.org/wiki/faq/exceptions#ctor-exceptions
        public ObjectCreationException(string? message) : base(message) { }
    }
}
