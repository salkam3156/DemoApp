using DemoApp.ApplicationCore.Exceptions;
using System;

namespace DemoApp.ApplicationCore.GeneralAbstractions
{
    public static class Validation
    {
        public static void ThrowIfAnyAreFalse(params Func<bool>[] validationPredicates)
        {
            foreach(var predicate in validationPredicates)
            {
                if (predicate is null) // being defensive here / futureproof
                    throw new ObjectCreationException("Invalid validation rule provided. Did you intend that ? The object cannot be created.");

                if(predicate.Invoke() is false) 
                    throw new ObjectCreationException("Validation failed while creating a domain object.");
            };
            
        }
    }
}
