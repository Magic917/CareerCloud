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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string connectionString = string.Empty;
        public CompanyProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Company_Profiles (id,Registration_Date,Company_Website, Contact_Phone, Contact_Name,Company_Logo)" +
                    " values (@id,@Registration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Company_Profiles";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<CompanyProfilePoco>();
                while (r.Read())
                {
                    
                    var Ins=new CompanyProfilePoco()
                    {
                        Id = (Guid)r["Id"],
                        RegistrationDate = (DateTime)r["Registration_Date"],
                                             
                        
                    };
                    if (r["Company_Website"] != DBNull.Value)
                    {
                        Ins.CompanyWebsite = (string)r["Company_Website"];
                    }
                    if (r["Contact_Phone"] != DBNull.Value)
                    {
                        Ins.ContactPhone = (string)r["Contact_Phone"];
                    }
                    if (r["Contact_Name"] != DBNull.Value)
                    {
                        Ins.ContactName = (string)r["Contact_Name"];
                    }

                    if (r["Company_Logo"] != DBNull.Value)
                    {
                        Ins.CompanyLogo = (byte[])r["Company_Logo"];
                    }
                    if (r["Time_Stamp"] != DBNull.Value)
                    {
                        Ins.TimeStamp = (byte[])r["Time_Stamp"];
                    }
                    l.Add( Ins );
                }
                return l;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Delete from Company_Profiles where id=@id ";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Company_Profiles set Registration_Date=@Registration_Date,Company_Website=@Company_Website, Contact_Phone=@Contact_Phone, Contact_Name=@Contact_Name,Company_Logo=@Company_Logo " +
                    " where id=@id ";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
