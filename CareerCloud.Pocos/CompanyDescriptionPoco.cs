﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table ("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }
        [Column("Company")]
        public Guid Company { get; set; }
        [Column("LanguageID")]
        public string LanguageId { get; set; }
        [Column("Company_Name")]
        public string CompanyName { get; set; }
        [Column("Company_Description")]
        public string CompanyDescription { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }


<<<<<<< HEAD
        [ForeignKey("Company")]
        public virtual CompanyProfilePoco? CompanyProfile { get; set; }
        public virtual SystemLanguageCodePoco? SystemLanguageCode { get; set; }
=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
