﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{

    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }
        [Column("Applicant")]
        public Guid Applicant { get; set; }
        [Column("Job")]
        public Guid Job { get; set; }
        [Column("Application_Date")]
        public DateTime ApplicationDate { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

<<<<<<< HEAD
        [ForeignKey("Applicant")]
        public virtual ApplicantProfilePoco? ApplicantProfile { get; set; }

        [ForeignKey("Job")]
        public virtual CompanyJobPoco? CompanyJob { get; set; }
=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
