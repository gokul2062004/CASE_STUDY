using System;

namespace CarRentalSystem.entity
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public string Status { get; set; } 
        public int PassengerCapacity { get; set; }
        public double EngineCapacity { get; set; }

        public Car() { }

        public Car(int carId, string make, string model, int year, decimal dailyRate, string status, int passengerCapacity, double engineCapacity)
        {
            CarId = carId;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }
    }
}
