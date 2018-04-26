using System;
using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int typeId, int customerId);
        bool AddCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        IEnumerable<CustomerType> GetCustomerTypes();
        bool PhoneNumberExists(string num);
        bool TypeExists(int id);
    }
}