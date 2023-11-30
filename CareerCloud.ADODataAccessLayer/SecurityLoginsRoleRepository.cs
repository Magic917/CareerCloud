﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string connectionString = string.Empty;
        public SecurityLoginsRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = "insert into Security_Logins_Roles (id,login,Role)" +
                    " values (@id,@Login, @Role)";
                conn.Open();
                foreach (var item in items)
                {
                    //cmd.Parameters.Clear();
                    
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    //cmd.Parameters.AddWithValue("@Time_Stamp", DateTime.UtcNow); this colum don't accept value, instead, it generated by DB
                    

                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Security_Logins_Roles";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<SecurityLoginsRolePoco>();
                while (r.Read())
                {
                    var _obj = new SecurityLoginsRolePoco()
                    {
                        Id = (Guid)r["id"],
                        Login = (Guid)r["Login"],
                        Role = (Guid)r["Role"],
                        TimeStamp = (byte[])r["Time_Stamp"],
                        
                    };

                    l.Add(_obj);
                }
                return l;
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete from Security_Logins_Roles where id=@id;";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update Security_Logins_Roles set login=@login, Role=@Role, Time_Stamp=@Time_Stamp where id=@id;";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@login", item.Login);
                    cmd.Parameters.AddWithValue("@Time_Stamp", item.TimeStamp);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}