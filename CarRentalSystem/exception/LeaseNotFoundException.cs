using System;

namespace CarRentalSystem.exception
{
    public class LeaseNotFoundException : Exception
    {
        public LeaseNotFoundException(string message) : base(message)
        {
        }
    }
}
