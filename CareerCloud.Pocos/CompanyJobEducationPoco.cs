﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco

    {
        [Key]
        public Guid Id { set; get; }
        [Column("Job")]
        public Guid Job { get; set; }
        [Column("Major")]
        public string Major { get; set; }
        [Column("Importance")]
        public short Importance { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }
<<<<<<< HEAD

        [ForeignKey("Job")]
        public virtual CompanyJobPoco? CompanyJob { get; set; }
=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
