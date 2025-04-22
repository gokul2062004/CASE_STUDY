using System;
using System.Collections.Generic;
using CarRentalSystem.entity;

namespace CarRentalSystem.dao
{
    public interface ICarLeaseRepository
    {
        // Car Management
        void AddCar(Car car);
        void RemoveCar(int carId);
        List<Car> ListAvailableCars();
        List<Car> ListRentedCars();
        Car FindCarById(int carId);

        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerId);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerId);
        Lease CreateLease(int customerId, int carId, DateTime startDate, DateTime endDate, string type);
        Lease ReturnCar(int leaseId);
        List<Lease> ListActiveLeases();
        List<Lease> ListLeaseHistory();
        void RecordPayment(Lease lease, double amount);
        decimal GetTotalRevenue();


    }
}
