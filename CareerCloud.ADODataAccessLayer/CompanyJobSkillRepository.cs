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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string connectionString = string.Empty;
        public CompanyJobSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            object value = config.AddJsonFile(path, false);
            var root = config.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Company_Job_Skills (id,Job,Skill, Skill_Level, Importance)" +
                    " values (@id,@Job,@Skill,@Skill_Level,@Importance)";
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Company_Job_Skills";
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                var l = new List<CompanyJobSkillPoco>();
                while (r.Read())
                {
                    var  cjs = new CompanyJobSkillPoco()
                    {
                        Id = (Guid)r["id"],
                        Job = (Guid)r["Job"],
                        Skill = (string)r["Skill"],
                        SkillLevel = (string)r["Skill_Level"],
                        Importance = (int)r["Importance"],
                        //TimeStamp = (byte[])r["Time_stamp"],
                        //TimeStamp = Convert.IsDBNull(r["Time_Stamp"]) ? null : (byte[])r["Time_stamp"],

                    };
                    if (r["Time_Stamp"]!=DBNull.Value)
                    {
                        cjs.TimeStamp = (byte[]?)r["Time_Stamp"];
                    }

                    l.Add(cjs);
                }
                return l;
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "Delete from Company_Job_Skills where id=@id ";
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "Update Company_Job_Skills set job=@Job, skill=@Skill, Skill_Level=@Skill_Level,Importance=@Importance where id=@id ";
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                   
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
