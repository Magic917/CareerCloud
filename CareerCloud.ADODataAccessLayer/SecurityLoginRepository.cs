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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>

    {
        private readonly string connectionString = string.Empty;
        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Security_Logins (id,Login,Password, Created_Date, Password_Update_date,Agreement_Accepted_date, " +
                    "Is_Locked,Is_Inactive,Email_Address,Phone_Number,Full_Name,Force_Change_Password,Prefferred_Language)" +
                    " values (@id,@Login,@Password,@Created_Date,@Password_Update_date,@Agreement_Accepted_date,@Is_Locked,@Is_Inactive,@Email_Address,@Phone_Number," +
                    "@Full_Name,@Force_Change_Password,@Prefferred_Language)";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Security_Logins";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<SecurityLoginPoco>();
                while (r.Read())
                {
                    var _obj = new SecurityLoginPoco()
                    {
                        Id = (Guid)r["id"],
                        Login = (string)r["Login"],
                        Password = (string)r["Password"],
                        Created = (DateTime)r["Created_Date"],
                        IsLocked = (bool)r["Is_Locked"],
                        IsInactive = (bool)r["Is_Inactive"],
                        EmailAddress = (string)r["Email_Address"],
                        FullName = (string)r["Full_Name"],
                        ForceChangePassword = (bool)r["Force_Change_Password"],
                        TimeStamp = (byte[])r["Time_stamp"]
                    };

                    if (r["Password_Update_date"] != DBNull.Value) 
                    {
                        _obj.PasswordUpdate = (DateTime?)r["Password_Update_date"];
                    }

                    if (r["Agreement_Accepted_date"] != DBNull.Value) 
                    {
                        _obj.AgreementAccepted = (DateTime?)r["Agreement_Accepted_date"];
                    }
                    if (r["Phone_Number"] != DBNull.Value)
                    {
                        _obj.PhoneNumber = (string)r["Phone_Number"];
                    }
                    if (r["Prefferred_Language"] != DBNull.Value)
                    {
                        _obj.PrefferredLanguage = (string)r["Prefferred_Language"];
                    }
                    l.Add( _obj );
                }
                return l;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete from Security_Logins where id=@id;";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update Security_Logins set Login=@Login,Password=@Password, Created_Date=@Created_Date, Password_Update_date=@Password_Update_date,Agreement_Accepted_date=@Agreement_Accepted_date, " +
                    "Is_Locked=@Is_Locked,Is_Inactive=@Is_Inactive,Email_Address=@Email_Address,Phone_Number=@Phone_Number,Full_Name=@Full_Name,Force_Change_Password=@Force_Change_Password,Prefferred_Language=@Prefferred_Language " +
                    " where id=@id";
                    
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);


                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
