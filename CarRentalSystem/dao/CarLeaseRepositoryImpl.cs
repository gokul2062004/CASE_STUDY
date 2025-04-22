using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarRentalSystem.entity;
using CarRentalSystem.util;
using CarRentalSystem.exception;

namespace CarRentalSystem.dao
{
    public class CarLeaseRepositoryImpl : ICarLeaseRepository
    {
        private SqlConnection connection;

        public CarLeaseRepositoryImpl()
        {
            connection = DBConnUtil.GetConnection();
        }

        public void AddCar(Car car)
        {
            string query = @"INSERT INTO Vehicle (make, model, year, dailyRate, status, passengerCapacity, engineCapacity)
                             VALUES (@Make, @Model, @Year, @DailyRate, @Status, @PassengerCapacity, @EngineCapacity)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Make", car.Make);
            cmd.Parameters.AddWithValue("@Model", car.Model);
            cmd.Parameters.AddWithValue("@Year", car.Year);
            cmd.Parameters.AddWithValue("@DailyRate", car.DailyRate);
            cmd.Parameters.AddWithValue("@Status", car.Status);
            cmd.Parameters.AddWithValue("@PassengerCapacity", car.PassengerCapacity);
            cmd.Parameters.AddWithValue("@EngineCapacity", car.EngineCapacity);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveCar(int carId)
        {
            string query = "DELETE FROM Vehicle WHERE vehicleID = @CarId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CarId", carId);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            connection.Close();
            if (rows == 0)
                throw new CarNotFoundException("Car ID not found.");
        }

        public List<Car> ListAvailableCars()
        {
            List<Car> cars = new List<Car>();
            string query = "SELECT * FROM Vehicle WHERE status = 'available'";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car(
                    Convert.ToInt32(reader["vehicleID"]),
                    reader["make"].ToString(),
                    reader["model"].ToString(),
                    Convert.ToInt32(reader["year"]),
                    Convert.ToDecimal(reader["dailyRate"]),
                    reader["status"].ToString(),
                    Convert.ToInt32(reader["passengerCapacity"]),
                    Convert.ToDouble(reader["engineCapacity"])
                );
                cars.Add(car);
            }
            reader.Close();
            connection.Close();
            return cars;
        }

        public List<Car> ListRentedCars()
        {
            List<Car> cars = new List<Car>();
            string query = "SELECT * FROM Vehicle WHERE status = 'notAvailable'";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car(
                    Convert.ToInt32(reader["vehicleID"]),
                    reader["make"].ToString(),
                    reader["model"].ToString(),
                    Convert.ToInt32(reader["year"]),
                    Convert.ToDecimal(reader["dailyRate"]),
                    reader["status"].ToString(),
                    Convert.ToInt32(reader["passengerCapacity"]),
                    Convert.ToDouble(reader["engineCapacity"])
                );
                cars.Add(car);
            }
            reader.Close();
            connection.Close();
            return cars;
        }

        public Car FindCarById(int carId)
        {
            string query = "SELECT * FROM Vehicle WHERE vehicleID = @CarId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CarId", carId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Car car = null;
            if (reader.Read())
            {
                car = new Car(
                    Convert.ToInt32(reader["vehicleID"]),
                    reader["make"].ToString(),
                    reader["model"].ToString(),
                    Convert.ToInt32(reader["year"]),
                    Convert.ToDecimal(reader["dailyRate"]),
                    reader["status"].ToString(),
                    Convert.ToInt32(reader["passengerCapacity"]),
                    Convert.ToDouble(reader["engineCapacity"])
                );
            }
            else
            {
                connection.Close();
                throw new CarNotFoundException("Car ID not found.");
            }
            reader.Close();
            connection.Close();
            return car;
        }

        public void AddCustomer(Customer customer)
        {
            string query = @"INSERT INTO Customer (firstName, lastName, email, phoneNumber)
                             VALUES (@FirstName, @LastName, @Email, @Phone)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Phone", customer.PhoneNumber);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveCustomer(int customerId)
        {
            string query = "DELETE FROM Customer WHERE customerID = @CustomerId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            connection.Close();
            if (rows == 0)
                throw new CustomerNotFoundException("Customer ID not found.");
        }

        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string query = "SELECT * FROM Customer";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Customer cust = new Customer(
                    Convert.ToInt32(reader["customerID"]),
                    reader["firstName"].ToString(),
                    reader["lastName"].ToString(),
                    reader["email"].ToString(),
                    reader["phoneNumber"].ToString()
                );
                customers.Add(cust);
            }
            reader.Close();
            connection.Close();
            return customers;
        }

        public Customer FindCustomerById(int customerId)
        {
            string query = "SELECT * FROM Customer WHERE customerID = @CustomerId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Customer cust = null;
            if (reader.Read())
            {
                cust = new Customer(
                    Convert.ToInt32(reader["customerID"]),
                    reader["firstName"].ToString(),
                    reader["lastName"].ToString(),
                    reader["email"].ToString(),
                    reader["phoneNumber"].ToString()
                );
            }
            else
            {
                connection.Close();
                throw new CustomerNotFoundException("Customer ID not found.");
            }
            reader.Close();
            connection.Close();
            return cust;
        }

        public Lease CreateLease(int customerId, int carId, DateTime startDate, DateTime endDate, string type)
        {
            connection.Open();

            // 1. Insert lease
            string query = @"INSERT INTO Lease (customerID, vehicleID, startDate, endDate, type)
                     VALUES (@CustomerId, @CarId, @StartDate, @EndDate, @Type)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            cmd.Parameters.AddWithValue("@CarId", carId);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.ExecuteNonQuery();

            // 2. Get lease ID
            SqlCommand getIdCmd = new SqlCommand("SELECT MAX(leaseID) FROM Lease", connection);
            int leaseId = Convert.ToInt32(getIdCmd.ExecuteScalar());

            // 3. Update vehicle status
            SqlCommand updateStatus = new SqlCommand("UPDATE Vehicle SET status = 'notAvailable' WHERE vehicleID = @CarId", connection);
            updateStatus.Parameters.AddWithValue("@CarId", carId);
            updateStatus.ExecuteNonQuery();

            connection.Close();

            return new Lease(leaseId, carId, customerId, startDate, endDate, type);
        }


        public Lease ReturnCar(int leaseId)
        {
            string query = "SELECT * FROM Lease WHERE leaseID = @LeaseId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LeaseId", leaseId);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Lease lease = null;

            if (reader.Read())
            {
                lease = new Lease(
                    Convert.ToInt32(reader["leaseID"]),
                    Convert.ToInt32(reader["vehicleID"]),
                    Convert.ToInt32(reader["customerID"]),
                    Convert.ToDateTime(reader["startDate"]),
                    Convert.ToDateTime(reader["endDate"]),
                    reader["type"].ToString()
                );
                reader.Close();

                Console.WriteLine("Updating status for vehicle ID: " + lease.VehicleId);

                // ✅ 1. Update car status
                SqlCommand updateCar = new SqlCommand(
                    "UPDATE Vehicle SET status = 'available' WHERE vehicleID = @CarId", connection);
                updateCar.Parameters.AddWithValue("@CarId", lease.VehicleId);
                int carRows = updateCar.ExecuteNonQuery();
                Console.WriteLine("Vehicle status update rows affected: " + carRows);

                // ✅ 2. Update lease status to 'completed'
                SqlCommand updateLease = new SqlCommand(
                    "UPDATE Lease SET status = 'completed' WHERE leaseID = @LeaseId", connection);
                updateLease.Parameters.AddWithValue("@LeaseId", lease.LeaseId);
                int leaseRows = updateLease.ExecuteNonQuery();
                Console.WriteLine("Lease status update rows affected: " + leaseRows);
            }
            else
            {
                reader.Close();
                connection.Close();
                throw new LeaseNotFoundException("Lease ID not found.");
            }

            connection.Close();
            return lease;
        }



        public List<Lease> ListActiveLeases()
        {
            List<Lease> leases = new List<Lease>();
            string query = "SELECT * FROM Lease WHERE status = 'active'";

            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                leases.Add(new Lease(
                    Convert.ToInt32(reader["leaseID"]),
                    Convert.ToInt32(reader["vehicleID"]),
                    Convert.ToInt32(reader["customerID"]),
                    Convert.ToDateTime(reader["startDate"]),
                    Convert.ToDateTime(reader["endDate"]),
                    reader["type"].ToString()
                ));
            }
            reader.Close();
            connection.Close();
            return leases;
        }

        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leases = new List<Lease>();
            string query = "SELECT * FROM Lease WHERE status = 'completed'";

            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                leases.Add(new Lease(
                    Convert.ToInt32(reader["leaseID"]),
                    Convert.ToInt32(reader["vehicleID"]),
                    Convert.ToInt32(reader["customerID"]),
                    Convert.ToDateTime(reader["startDate"]),
                    Convert.ToDateTime(reader["endDate"]),
                    reader["type"].ToString()
                ));
            }
            reader.Close();
            connection.Close();
            return leases;
        }


        public void RecordPayment(Lease lease, double amount)
        {
            string query = @"INSERT INTO Payment (leaseID, paymentDate, amount)
                             VALUES (@LeaseId, @Date, @Amount)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LeaseId", lease.LeaseId);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Amount", amount);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public decimal GetTotalRevenue()
        {
            string query = "SELECT SUM(amount) FROM Payment";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            object result = cmd.ExecuteScalar();
            connection.Close();

            if (result == DBNull.Value)
                return 0;
            return Convert.ToDecimal(result);
        }

    }
}
