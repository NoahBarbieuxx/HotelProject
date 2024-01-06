using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("GetAllCustomer | Status = 1", ex);
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                _customerRepository.AddCustomer(customer);
            } catch (Exception ex)
            {
                throw new CustomerManagerException("AddCustomer", ex);
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            try
            {
                _customerRepository.DeleteCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("DeleteCustomer", ex);
            }
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                return _customerRepository.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetCustomerById", ex);

            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("UpdateCustomer", ex);
            }
        }

        public bool CustomerExists(int id)
        {
            try
            {
                return _customerRepository.CustomerExists(id);
            } catch (Exception ex)
            {
                throw new CustomerManagerException("CustomerExists", ex);
            }
        }
    }
}