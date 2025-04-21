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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string connectionString = string.Empty;
        public CompanyLocationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Company_Locations (id,company,Country_Code, State_Province_code, Street_Address,City_Town,Zip_Postal_Code)" +
                    " values (@id,@company,@Country_Code,@State_Province_code,@Street_Address,@City_Town,@Zip_Postal)";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@company", item.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal", item.PostalCode);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Company_Locations";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<CompanyLocationPoco>();
                while (r.Read())
                {
                    var TInstance = new CompanyLocationPoco()
                    {
                        Id = (Guid)r["id"],
                        Company = (Guid)r["company"],
                        CountryCode = (string)r["Country_Code"],
                        Province = (string)r["State_Province_code"],
                        Street = (string)r["Street_Address"],

                        TimeStamp = (byte[])r["Time_stamp"]
                    };
                    if (r["City_Town"]!=DBNull.Value)
                    {
                        TInstance.City = (string)r["City_Town"];
                    }
                    if (r["Zip_Postal_Code"] != DBNull.Value)
                    {
                        TInstance.PostalCode = (string)r["Zip_Postal_Code"];
                    }

                    l.Add(TInstance);
                   
                }
                return l;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn= new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "delete from Company_Locations where id=@id";
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                
            }
            
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn= new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "update Company_Locations set company=@company,Country_Code=@Country_Code,State_Province_code=@State_Province_code,Street_Address=@Street_Address,City_Town=@City_Town,Zip_Postal_Code=@Zip_Postal_Code where id=@id";
                cmd.Connection = conn;
                conn.Open();
                
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@company", item.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    cmd.ExecuteNonQuery();
                }
            }
            
        }
    }
}
