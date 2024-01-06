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

namespace HotelProject.DL.Repositories
{
    public class OrganiserRepositoryADO : IOrganiserRepository
    {
        private string connectionString;

        public OrganiserRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Organiser> GetOrganisers()
        {
            try
            {
                List<Organiser> Organisers = new List<Organiser>();
                string sql = "SELECT * FROM Organiser";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Organiser organiser = new Organiser(
                            (int)reader["id"],
                            (string)reader["name"],
                            new ContactInfo(
                                (string)reader["email"],
                                (string)reader["phone"],
                                new Address((string)reader["address"])
                            )
                        );
                        Organisers.Add(organiser);
                    }
                    return Organisers;
                }
            }
            catch (Exception ex)
            {
                throw new OrganiserRepositoryException("GetOrganisers", ex);
            }
        }

        public void AddOrganiser(Organiser organiser)
        {
            try
            {
                string SQL = "INSERT INTO Organiser(name,email,phone,address) output INSERTED.id VALUES(@name,@email,@phone,@address)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@name", organiser.Name);
                        cmd.Parameters.AddWithValue("@email", organiser.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", organiser.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", organiser.ContactInfo.Address.ToAddressLine());
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
                throw new OrganiserRepositoryException("AddOrganiser", ex);
            }
        }
    }
}