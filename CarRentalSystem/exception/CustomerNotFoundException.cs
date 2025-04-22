using System;

namespace CarRentalSystem.exception
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message)
        {
        }
    }
}
