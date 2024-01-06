using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using HotelProject.BL.Exceptions;

namespace HotelProject.DL.Repositories
{
    public class RegistrationRepositoryADO : IRegistrationRepository
    {
        private string _connectionString;
        private ICustomerRepository _customerRepository;
        private IActivityRepository _activityRepository;

        public RegistrationRepositoryADO(string connectionString, ICustomerRepository customerRepository, IActivityRepository activityRepository)
        {
            _connectionString = connectionString;
            _customerRepository = customerRepository;
            _activityRepository = activityRepository;
        }

        public void AddRegistration(Registration registration)
        {
            try
            {
                string SQL = "INSERT INTO Registrations(activityId, customerId, memberCount, cost) OUTPUT INSERTED.registrationId VALUES(@activityId, @customerId, @memberCount, @cost)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = null;
                    SqlTransaction transaction2 = null;

                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = SQL;

                            cmd.Parameters.AddWithValue("@activityId", registration.Activity.Id);
                            cmd.Parameters.AddWithValue("@customerId", registration.Customer.Id);
                            cmd.Parameters.AddWithValue("@memberCount", registration.GetMembers().Count);
                            cmd.Parameters.AddWithValue("@cost", registration.Cost());

                            transaction = conn.BeginTransaction();
                            cmd.Transaction = transaction;

                            int id = (int)cmd.ExecuteScalar();

                            transaction.Commit();

                            Customer customer = _customerRepository.GetCustomerById(registration.Customer.Id);

                            string SQL_GET = "SELECT * FROM Registrations WHERE registrationId = @id";
                            using (SqlCommand cmd2 = conn.CreateCommand())
                            {
                                try
                                {
                                    cmd2.CommandText = SQL_GET;
                                    cmd2.Parameters.AddWithValue("@id", id);

                                    transaction2 = conn.BeginTransaction();
                                    cmd2.Transaction = transaction2;

                                    using (IDataReader dr = cmd2.ExecuteReader())
                                    {
                                        if (dr.Read())
                                        {
                                            int activityId = (int)dr["activityId"];
                                            List<Activity> activities = _activityRepository.GetAllActivities();
                                            Activity activityCorrect = null;

                                            foreach (Activity activity in activities)
                                            {
                                                if (activity.Id == activityId)
                                                {
                                                    activityCorrect = new Activity(activity.Id, activity.ActivityDescription, activity.Fixture, activity.NumberOfPlaces, activity.PriceInfo);
                                                }
                                            }

                                            int customerId = (int)dr["customerId"];
                                            Customer customerCorrect = _customerRepository.GetCustomerById(customerId);

                                            Registration r = new Registration(
                                                (int)dr["registrationId"],
                                                activityCorrect,
                                                customerCorrect,
                                                (decimal)dr["cost"]
                                            );

                                            string SQL_ADD = "INSERT INTO MemberRegistrations(memberName, registrationId) OUTPUT INSERTED.memberRegistrationId VALUES(@memberName, @registrationId)";
                                            using (SqlCommand cmd3 = conn.CreateCommand())
                                            {
                                                dr.Close();

                                                cmd3.CommandText = SQL_ADD;
                                                cmd3.Transaction = transaction2;

                                                foreach (Member member in registration.GetMembers())
                                                {
                                                    cmd3.Parameters.Clear();
                                                    cmd3.Parameters.AddWithValue("@memberName", member.Name);
                                                    cmd3.Parameters.AddWithValue("@registrationId", r.Id);

                                                    int memberRegistrationId = (int)cmd3.ExecuteScalar();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            throw new RegistrationRepositoryException("No rows found for registrationId: " + id);
                                        }
                                    }

                                    transaction2.Commit();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    if (transaction2 != null)
                                    {
                                        transaction2.Rollback();
                                    }
                                    throw new RegistrationRepositoryException("GetRegistrationsById", ex);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }
                        throw new RegistrationRepositoryException("AddRegistration", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException("AddRegistration", ex);
            }
        }

        public List<Registration> GetRegistrationByActivityId(int activityId)
        {
            try
            {
                List<Registration> registrations = new List<Registration>();

                string query = "SELECT * FROM Registrations WHERE activityId = @activityId";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@activityId", activityId);
                        IDataReader dr = cmd.ExecuteReader();

                        Registration registration = null;

                        while (dr.Read())
                        {
                            if (registration == null)
                            {
                                int activityID = (int)dr["activityId"];
                                List<Activity> activities = _activityRepository.GetAllActivities();
                                Activity activityCorrect = null;

                                foreach (Activity activity in activities)
                                {
                                    if (activity.Id == activityId)
                                    {
                                        activityCorrect = new Activity(activity.Id, activity.ActivityDescription, activity.Fixture, activity.NumberOfPlaces, activity.PriceInfo);
                                    }
                                }

                                int customerId = (int)dr["customerId"];
                                Customer customerCorrect = _customerRepository.GetCustomerById(customerId);

                                registration = new Registration((int)dr["registrationId"], activityCorrect, customerCorrect, (decimal)dr["cost"]);

                                registrations.Add(registration);
                            }
                        }

                        dr.Close();
                        return registrations;
                    }
                    catch (Exception ex)
                    {
                        throw new RegistrationRepositoryException("GetRegistrationByActivityId", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException("GetRegistrationByActivityId", ex);
            }
        }


        public bool MemberRegistrated(string name, int registrationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM MemberRegistrations WHERE memberName = @Name AND registrationId = @RegistrationId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@RegistrationId", registrationId);

                        int count = (int)cmd.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException("MemberRegistrated", ex);
            }
        }
    }
}