using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string connectionString = string.Empty;
        public ApplicantJobApplicationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using( var connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Applicant_Job_Applications (id,applicant,job, application_date)" +
                    " values (@id,@applicant,@job,@application_date)";
                cmd.Connection = connection;

                connection.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@job", item.Job);
                    cmd.Parameters.AddWithValue("@application_date", item.ApplicationDate);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd= conn.CreateCommand();
                cmd.CommandText = "select * from Applicant_Job_Applications";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<ApplicantJobApplicationPoco>();
                while (r.Read())
                {
                    l.Add(new ApplicantJobApplicationPoco()
                    {
                        Id = (Guid)r["id"],
                        Applicant = (Guid)r["applicant"],
                        Job = (Guid)r["job"],
                        ApplicationDate = (DateTime)r["Application_Date"],
                        TimeStamp = (byte[])r["Time_stamp"],
                        
                    });
                }
                return l;

            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete Applicant_Job_Applications where id=@id";
                cmd.Connection = connection;

                connection.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@job", item.Job);
                    cmd.Parameters.AddWithValue("@application_date", item.ApplicationDate);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update Applicant_Job_Applications set applicant=@applicant,job=@job, application_date=@application_date where id=@id";
                cmd.Connection = connection;

                connection.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@job", item.Job);
                    cmd.Parameters.AddWithValue("@application_date", item.ApplicationDate);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
