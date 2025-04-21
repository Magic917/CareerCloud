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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string connectionString = string.Empty;
        public SecurityLoginsLogRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Security_Logins_Log (id,Login,Source_IP, Logon_Date, Is_Succesful )" +
                    " values (@id,@Login,@Source_IP,@Logon_Date,@Is_Succesful)";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Security_Logins_Log";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<SecurityLoginsLogPoco>();
                while (r.Read())
                {
                    var _obj = new SecurityLoginsLogPoco()
                    {
                        Id = (Guid)r["id"],
                        Login = (Guid)r["Login"],
                        SourceIP = (string)r["Source_IP"],
                        LogonDate = (DateTime)r["Logon_Date"],
                        IsSuccesful = (bool)r["Is_Succesful"],
                     
                    };
       
                    l.Add(_obj);
                }
                return l;
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from Security_Logins_Log where id=@id;  ";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Security_Logins_Log set Login=@Login,Source_IP=@Source_IP, Logon_Date=@Logon_Date, Is_Succesful=@Is_Succesful " +
                    " where id=@id";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
