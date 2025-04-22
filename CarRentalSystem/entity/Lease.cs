using System;

namespace CarRentalSystem.entity
{
    public class Lease
    {
        public int LeaseId { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; } // DailyLease or MonthlyLease

        public Lease() { }

        public Lease(int leaseId, int vehicleId, int customerId, DateTime startDate, DateTime endDate, string type)
        {
            LeaseId = leaseId;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
        }
    }
}
