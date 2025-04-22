using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }

        [Column("Job")]
        public Guid Job { get; set; }
        [Column("Skill")]
        public string  Skill { get; set; }
        [Column("Skill_Level")]
        public string SkillLevel { get; set; }
        [Column("Importance")]
        public int Importance { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

<<<<<<< HEAD
        [ForeignKey("Job")]
        public virtual CompanyJobPoco? CompanyJob { get; set; }

=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
