using CarRentalSystem.dao;
using CarRentalSystem.entity;
using CarRentalSystem.exception;
using System;
using System.Collections.Generic;

namespace CarRentalSystem.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            ICarLeaseRepository repo = new CarLeaseRepositoryImpl();

            while (true)
            {
                Console.WriteLine("\n===== CAR RENTAL SYSTEM =====");
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Create Lease");
                Console.WriteLine("4. Return Car");
                Console.WriteLine("5. List Available Cars");
                Console.WriteLine("6. List Customers");
                Console.WriteLine("7. Record Payment");
                Console.WriteLine("8. View Total Revenue");
                Console.WriteLine("9. Exit");
                Console.WriteLine("10. Remove Car");
                Console.WriteLine("11. Remove Customer");
                Console.WriteLine("12. Find Car By ID");
                Console.WriteLine("13. Find Customer By ID");
                Console.WriteLine("14. List Active Leases");
                Console.WriteLine("15. List Lease History");
                Console.WriteLine("16. List Rented Cars");



                Console.Write("Choose option: ");
                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Make: ");
                            string make = Console.ReadLine();
                            Console.Write("Model: ");
                            string model = Console.ReadLine();
                            Console.Write("Year: ");
                            int year = int.Parse(Console.ReadLine());
                            Console.Write("Rate: ");
                            decimal rate = decimal.Parse(Console.ReadLine());
                            Console.Write("Status: ");
                            string status = Console.ReadLine();
                            Console.Write("Passenger Capacity: ");
                            int pc = int.Parse(Console.ReadLine());
                            Console.Write("Engine Capacity: ");
                            double ec = double.Parse(Console.ReadLine());

                            Car car = new Car(0, make, model, year, rate, status, pc, ec);
                            repo.AddCar(car);
                            Console.WriteLine("Car added successfully.");
                            break;

                        case 2:
                            Console.Write("First Name: ");
                            string fname = Console.ReadLine();
                            Console.Write("Last Name: ");
                            string lname = Console.ReadLine();
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Phone: ");
                            string phone = Console.ReadLine();

                            Customer customer = new Customer(0, fname, lname, email, phone);
                            repo.AddCustomer(customer);
                            Console.WriteLine("Customer added successfully.");
                            break;

                        case 3:
                            Console.Write("Customer ID: ");
                            int custId = int.Parse(Console.ReadLine());
                            Console.Write("Car ID: ");
                            int carId = int.Parse(Console.ReadLine());
                            Console.Write("Start Date (yyyy-mm-dd): ");
                            DateTime start = DateTime.Parse(Console.ReadLine());
                            Console.Write("End Date (yyyy-mm-dd): ");
                            DateTime end = DateTime.Parse(Console.ReadLine());
                            Console.Write("Type (DailyLease/MonthlyLease): ");
                            string type = Console.ReadLine();

                            Lease lease = repo.CreateLease(custId, carId, start, end, type);
                            Console.WriteLine("Lease created with ID: " + lease.LeaseId);
                            break;

                        case 4:
                            Console.Write("Enter Lease ID to return: ");
                            int leaseId = int.Parse(Console.ReadLine());
                            Lease returned = repo.ReturnCar(leaseId);
                            Console.WriteLine("Car returned successfully.");
                            break;

                        case 5:
                            List<Car> availableCars = repo.ListAvailableCars();
                            Console.WriteLine("Available Cars:");
                            foreach (var c in availableCars)
                            {
                                Console.WriteLine($"{c.CarId} - {c.Make} {c.Model} ({c.Year}) - ₹{c.DailyRate}/day");
                            }
                            break;

                        case 6:
                            List<Customer> customers = repo.ListCustomers();
                            Console.WriteLine("Customers:");
                            foreach (var cust in customers)
                            {
                                Console.WriteLine($"{cust.CustomerId} - {cust.FirstName} {cust.LastName} ({cust.Email})");
                            }
                            break;

                        case 7:
                            Console.Write("Enter Lease ID: ");
                            int lId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Payment Amount: ");
                            double amount = double.Parse(Console.ReadLine());
                            Lease payLease = repo.ReturnCar(lId);
                            repo.RecordPayment(payLease, amount);
                            Console.WriteLine("Payment recorded.");
                            break;

                        case 8:
                            decimal totalRevenue = repo.GetTotalRevenue();
                            Console.WriteLine("Total Revenue Collected: ₹" + totalRevenue);
                            break;


                        case 9:
                            return;
                        case 10:
                            Console.Write("Enter Car ID to remove: ");
                            int removeCarId = int.Parse(Console.ReadLine());
                            repo.RemoveCar(removeCarId);
                            Console.WriteLine("Car removed.");
                            break;

                        case 11:
                            Console.Write("Enter Customer ID to remove: ");
                            int removeCustId = int.Parse(Console.ReadLine());
                            repo.RemoveCustomer(removeCustId);
                            Console.WriteLine("Customer removed.");
                            break;

                        case 12:
                            Console.Write("Enter Car ID to find: ");
                            int carIdToFind = int.Parse(Console.ReadLine());
                            Car foundCar = repo.FindCarById(carIdToFind);
                            Console.WriteLine($"{foundCar.CarId}: {foundCar.Make} {foundCar.Model} - {foundCar.Status}");
                            break;

                        case 13:
                            Console.Write("Enter Customer ID to find: ");
                            int custIdToFind = int.Parse(Console.ReadLine());
                            Customer foundCust = repo.FindCustomerById(custIdToFind);
                            Console.WriteLine($"{foundCust.CustomerId}: {foundCust.FirstName} {foundCust.LastName} - {foundCust.Email}");
                            break;

                        case 14:
                            List<Lease> activeLeases = repo.ListActiveLeases();
                            Console.WriteLine("Active Leases:");
                            foreach (var l in activeLeases)
                            {
                                Console.WriteLine($"Lease ID: {l.LeaseId}, Car ID: {l.VehicleId}, Customer ID: {l.CustomerId}");
                            }
                            break;

                        case 15:
                            List<Lease> leaseHistory = repo.ListLeaseHistory();
                            Console.WriteLine("Lease History:");
                            foreach (Lease l in leaseHistory)
                            {
                                Console.WriteLine($"Lease ID: {l.LeaseId}, Car ID: {l.VehicleId}, Customer ID: {l.CustomerId}, From: {l.StartDate.ToShortDateString()} To: {l.EndDate.ToShortDateString()}, Type: {l.Type}");
                            }

                            break;

                        case 16:
                            List<Car> rentedCars = repo.ListRentedCars();
                            Console.WriteLine("Rented Cars:");
                            foreach (var c in rentedCars)
                            {
                                Console.WriteLine($"{c.CarId} - {c.Make} {c.Model} ({c.Year}) - ₹{c.DailyRate}/day");
                            }
                            break;






                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (CarNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (CustomerNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (LeaseNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected Error: " + ex.Message);
                }
            }
        }
    }
}