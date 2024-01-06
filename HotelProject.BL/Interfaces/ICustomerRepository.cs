using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        bool CustomerExists(int id);
        List<Customer> GetCustomers(string filter);
        Customer GetCustomerById(int id);
    }
}
