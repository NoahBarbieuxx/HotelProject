using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class CustomerRepositoryADO : ICustomerRepository
    {
        private string _connectionString;

        public CustomerRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                string sql;
                if (string.IsNullOrEmpty(filter))
                {
                    sql = "SELECT t1.id, t1.email, t1.name AS customername, t1.address, t1.phone, t2.name AS membername, t2.birthday FROM customer t1 LEFT JOIN (SELECT * FROM Members WHERE status = 1) t2 ON t1.id = t2.customerId WHERE t1.status = 1;";
                }
                else
                {
                    sql = "select t1.id,t1.email,t1.name customername,t1.address,t1.phone,t2.name membername,t2.birthday from customer t1 left join (select * from Members where status=1) t2 on t1.id=t2.customerId where t1.status=1 and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }
                using(SqlConnection conn = new SqlConnection(_connectionString)) 
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    SqlDataReader reader=cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id= Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id))
                            {
                               customers.Add(id, new Customer((string)reader["customername"], (int)reader["id"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"]))));                              
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                customers[id].AddMember(new Member((string)reader["membername"], (DateTime)reader["birthday"]));
                            }                            
                        }
                    return customers.Values.ToList();
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomer",ex);
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                string SQL = "INSERT INTO Customer(name, email, phone, address, status) OUTPUT INSERTED.id VALUES(@name, @email, @phone, @address, @status)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SQL, conn, transaction))
                        {
                            if (!CustomerExists(customer.Id))
                            {
                                cmd.Parameters.AddWithValue("@name", customer.Name);
                                cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                                cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                                cmd.Parameters.AddWithValue("@address", customer.ContactInfo.Address.ToAddressLine());
                                cmd.Parameters.AddWithValue("@status", 1);

                                int customerId = (int)cmd.ExecuteScalar();

                                SQL = "INSERT INTO Members(name, birthday, status, customerid) VALUES(@name, @birthday, @status, @customerid)";
                                cmd.CommandText = SQL;

                                foreach (Member member in customer.GetMembers())
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@name", member.Name);
                                    cmd.Parameters.AddWithValue("@birthday", member.BirthDay);
                                    cmd.Parameters.AddWithValue("@customerid", customerId);
                                    cmd.Parameters.AddWithValue("@status", 1);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {

                                SQL = "INSERT INTO Members(name, birthday, status, customerid) VALUES(@name, @birthday, @status, @customerid)";
                                cmd.CommandText = SQL;

                                foreach (Member member in customer.GetMembers())
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@name", member.Name);
                                    cmd.Parameters.AddWithValue("@birthday", member.BirthDay);
                                    cmd.Parameters.AddWithValue("@customerid", customer.Id);
                                    cmd.Parameters.AddWithValue("@status", 1);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("AddCustomer", ex);
            }
        }


        public void DeleteCustomer(Customer customer)
        {
            string sql = "UPDATE Customer SET status=@status WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", customer.Id);
                    cmd.Parameters.AddWithValue("@status", 0);
                    cmd.ExecuteNonQuery();

                    sql = "UPDATE Members SET status=@status WHERE customerId=@customerId";
                    cmd.CommandText = sql;
                    foreach (Member member in customer.GetMembers())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@customerId", customer.Id);
                        cmd.Parameters.AddWithValue("@status", 0);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new CustomerRepositoryException("UpdateCustomer", ex);
                }
            }
        }

        public Customer GetCustomerById(int id)
        {
            string sql = "SELECT c.id, c.name, c.email, c.phone, c.address, m.name AS membername, m.birthday FROM Customer c LEFT JOIN Members m ON c.id = m.customerId WHERE c.id = @id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader dr = cmd.ExecuteReader();

                    Customer customer = null;

                    while (dr.Read())
                    {
                        if (customer == null)
                        {
                            customer = new Customer((string)dr["name"], (int)dr["id"], new ContactInfo((string)dr["email"], (string)dr["phone"], new Address((string)dr["address"])));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("membername")))
                        {
                            customer.AddMember(new Member((string)dr["membername"], (DateTime)dr["birthday"]));
                        }
                    }

                    dr.Close();
                    return customer;
                }
                catch (Exception ex)
                {
                    throw new CustomerRepositoryException("GetCustomerById", ex);
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            string sql = "UPDATE Customer SET name=@name, email=@email, phone=@phone WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", customer.Id);
                    cmd.Parameters.AddWithValue("@name", customer.Name);
                    cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                    cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)                {
                    throw new CustomerRepositoryException("UpdateCustomer", ex);
                }
            }
        }

        public bool CustomerExists(int id)
        {
            bool exists = false;

            string sql = "SELECT COUNT(*) FROM Customer WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                int count = (int)cmd.ExecuteScalar();

                exists = count > 0;
            }

            return exists;
        }
    }
}