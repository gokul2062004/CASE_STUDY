using System;

namespace CarRentalSystem.entity
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int LeaseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public Payment() { }

        public Payment(int paymentId, int leaseId, DateTime paymentDate, decimal amount)
        {
            PaymentId = paymentId;
            LeaseId = leaseId;
            PaymentDate = paymentDate;
            Amount = amount;
        }
    }
}
