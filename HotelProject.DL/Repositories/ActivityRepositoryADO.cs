using HotelProject.BL.Interfaces;
using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class ActivityRepositoryADO : IActivityRepository
    {
        private string _connectionString;

        public ActivityRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddActivity(Activity activity)
        {
            try
            {
                string sql = "INSERT INTO Activities(description, location, duration, name, date, avalaibleSpots, adultCost, childCost, adultAge, discount) OUTPUT INSERTED.id VALUES(@description, @location, @duration, @name, @date, @avalaibleSpots, @adultCost, @childCost, @adultAge, @discount)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@description", activity.ActivityDescription.Description);
                        cmd.Parameters.AddWithValue("@location", activity.ActivityDescription.Location);
                        cmd.Parameters.AddWithValue("@duration", activity.ActivityDescription.Duration);
                        cmd.Parameters.AddWithValue("@name", activity.ActivityDescription.Name);
                        cmd.Parameters.AddWithValue("@date", activity.Fixture);
                        cmd.Parameters.AddWithValue("@avalaibleSpots", activity.NumberOfPlaces);
                        cmd.Parameters.AddWithValue("@adultCost", activity.PriceInfo.AdultCost);
                        cmd.Parameters.AddWithValue("@childCost", activity.PriceInfo.ChildCost);
                        cmd.Parameters.AddWithValue("@adultAge", activity.PriceInfo.AdultAge);
                        cmd.Parameters.AddWithValue("@discount", activity.PriceInfo.Discount);
                        int id = (int)cmd.ExecuteScalar();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ActivitiesRepositoryException("AddActivity", ex);
            }
        }

        public List<Activity> GetAllActivities()
        {
            try
            {
                List<Activity> activities = new List<Activity>();
                string sql = "SELECT * FROM Activities";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Activity activity = new Activity(
                            (int)reader["id"],
                            new ActivityDescription((string)reader["name"], (string)reader["location"], (int)reader["duration"], (string)reader["description"]),
                            (DateTime)reader["date"],
                            (int)reader["avalaibleSpots"],
                            new PriceInfo((decimal)reader["adultCost"], (decimal)reader["childCost"], (decimal)reader["discount"], (int)reader["adultAge"])
                        ); 

                        activities.Add(activity);
                    }
                    return activities;
                }
            }
            catch (Exception ex)
            {
                throw new ActivitiesRepositoryException("GetAllActivities", ex);
            }
        }

        public void UpdateActivity(Activity activity)
        {
            string sql = "UPDATE Activities SET avalaibleSpots=@avalaibleSpots WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", activity.Id);
                    cmd.Parameters.AddWithValue("@avalaibleSpots", activity.NumberOfPlaces);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CustomerRepositoryException("UpdateCustomer", ex);
                }
            }
        }
    }
}