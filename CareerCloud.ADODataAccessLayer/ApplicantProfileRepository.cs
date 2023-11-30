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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string connectionString = string.Empty;
        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
            
                cmd.CommandText = "insert into Applicant_Profiles (id,login,Current_Salary, Current_Rate, Currency,Country_Code,State_Province_Code,street_Address,city_town ,Zip_Postal_Code)" +
                    " values (@id,@login,@CurrentSalary,@CurrentRate,@Currency,@Country,@Province,@street,@city,@PostCode)";
                
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@login", item.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country", item.Country);
                    cmd.Parameters.AddWithValue("@Province", item.Province);
                    cmd.Parameters.AddWithValue("@street", item.Street);
                    cmd.Parameters.AddWithValue("@city", item.City);
                    cmd.Parameters.AddWithValue("@PostCode", item.PostalCode);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Applicant_Profiles";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<ApplicantProfilePoco>();
                while (r.Read())
                {
                    l.Add(new ApplicantProfilePoco()
                    {
                        Id = (Guid)r["id"],
                        Login = (Guid)r["login"],
                        CurrentSalary = (decimal?)r["current_salary"],
                        CurrentRate = (decimal?)r["current_rate"],
                        Currency = (string)r["currency"],
                        Country = (string)r["Country_Code"],
                        Province= (string)r["State_Province_Code"],
                        Street = (string)r["Street_Address"],
                        City = (string)r["City_Town"],
                        PostalCode = (string)r["Zip_Postal_Code"],
                        TimeStamp = (byte[])r["Time_stamp"],
                        //Applicant= new applican
                    }); ;
                }
                return l;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from Applicant_Profiles where id=@id";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update Applicant_Profiles set login=@login,Current_Salary=@CurrentSalary, Current_Rate=@CurrentRate, Currency=@Currency,Country_code=@Country,State_Province_Code=@Province,street_Address=@street,city_town=@city ,Zip_Postal_Code=@PostCode" +
                    " where id=@id";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@login", item.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country", item.Country);
                    cmd.Parameters.AddWithValue("@Province", item.Province);
                    cmd.Parameters.AddWithValue("@street", item.Street);
                    cmd.Parameters.AddWithValue("@city", item.City);
                    cmd.Parameters.AddWithValue("@PostCode", item.PostalCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
