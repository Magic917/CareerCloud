﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }
        [Column("Job")]
        public Guid Job { get; set; }
        [Column("Job_Name")]
        public string JobName { get; set; }
        [Column("Job_Descriptions")]
        public string JobDescriptions { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }
<<<<<<< HEAD
        public virtual CompanyJobPoco? CompanyJob { get; set; }
=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
