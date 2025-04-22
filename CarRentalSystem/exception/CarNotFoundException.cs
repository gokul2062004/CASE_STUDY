using System;

namespace CarRentalSystem.exception
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException(string message) : base(message)
        {
        }
    }
}
