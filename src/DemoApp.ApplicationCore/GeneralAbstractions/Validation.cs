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
                if(predicate.Invoke() is false) 
                    throw new ObjectCreationException("Validation failed while creating a domain object.");
            };
        }
    }
}
