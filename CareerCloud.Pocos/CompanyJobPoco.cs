﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }
        [Column("Company")]
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }
<<<<<<< HEAD

        public virtual ICollection<ApplicantJobApplicationPoco>? ApplicantJobApplications { get; set; }
        public virtual ICollection<CompanyJobEducationPoco>? CompanyJobEducations { get; set; }
        public virtual ICollection<CompanyJobSkillPoco>? CompanyJobSkills { get; set; }
        public virtual ICollection<CompanyJobDescriptionPoco>? CompanyJobDescriptions { get; set; }

        [ForeignKey("Company")]
        public virtual CompanyProfilePoco? CompanyProfile { get; set; }
=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
