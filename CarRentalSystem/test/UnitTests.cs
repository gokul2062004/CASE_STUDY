using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRentalSystem.dao;
using CarRentalSystem.entity;
using CarRentalSystem.exception;
using System;
using System.Collections.Generic;

namespace CarRentalSystem.test
{
    [TestClass]
    public class UnitTests
    {
        ICarLeaseRepository repo;

        [TestInitialize]
        public void Setup()
        {
            repo = new CarLeaseRepositoryImpl();
        }

        [TestMethod]
        public void TestAddCar()
        {
            Car car = new Car(0, "Test", "ModelX", 2022, 500, "available", 4, 2.0);
            repo.AddCar(car);
        }

        
        

        [TestMethod]
        public void TestCreateLease()
        {
            // Assumes CarId 1 and CustomerId 1 already exist
            Lease lease = repo.CreateLease(1, 1, DateTime.Today, DateTime.Today.AddDays(5), "DailyLease");
            Assert.IsNotNull(lease);
        }


        [TestMethod]
        public void TestInvalidCarThrowsException()
        {
            Assert.ThrowsExactly<CarNotFoundException>(() => repo.FindCarById(3));
        }


        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void TestInvalidCustomerThrowsException()
        {
            repo.FindCustomerById(99999);
        }

        [TestMethod]
        [ExpectedException(typeof(LeaseNotFoundException))]
        public void TestInvalidLeaseThrowsException()
        {
            repo.ReturnCar(99999);
        }
    }
}
