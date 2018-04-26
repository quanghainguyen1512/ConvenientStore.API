using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class CustomerRepository : ConvenientStoreRepository, Interfaces.ICustomerRepository
    {
        public CustomerRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AddCustomer(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteCustomer(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetCustomer(int typeId, int customerId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT * FROM customer as c " +
                    "INNER JOIN customer_type as ct ON c.TypeId = ct.Id " +
                    "WHERE c.CustomerId = @customerId AND c.TypeId = @typeId";
                return conn.Query<Customer, CustomerType, Customer>(
                    sql,
                    map: (c, ct) =>
                    {
                        c.CustomerType = ct;
                        return c;
                    },
                    param : new { customerId, typeId }
                ).FirstOrDefault();
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT * FROM customer";
                return conn.Query<Customer>(sql);
            }
        }

        public IEnumerable<CustomerType> GetCustomerTypes()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT * FROM customer_type as ct " +
                    "INNER JOIN customer as c ON ct.Id = c.TypeId";
                var dict = new Dictionary<int, CustomerType>();

                return conn.Query<CustomerType, Customer, CustomerType>(
                    sql,
                    map: (ct, c) =>
                    {
                        if (!dict.TryGetValue(ct.Id, out var entry))
                        {
                            entry = ct;
                            entry.Customers = new List<Customer>();
                            dict.Add(entry.Id, entry);
                        }

                        entry.Customers.Add(c);
                        return entry;
                    },
                    splitOn: "CustomerId"
                ).Distinct();
            }
        }
        public bool PhoneNumberExists(string num)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT PhoneNumber FROM customer " +
                    "WHERE PhoneNumber = @num";
                return conn.ExecuteScalar(sql, param : new { num }) != null;
            }
        }

        public bool TypeExists(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT Id FROM customer_type WHERE Id = @id";
                return conn.ExecuteScalar(sql, param : new { id }) != null;
            }
        }
    }
}