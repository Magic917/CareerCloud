﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Log")]
    public  class SecurityLoginsLogPoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }
        [Column("Login")]
        public Guid Login { get; set; }
        [Column("Source_IP")]
        public string SourceIP { get; set; }
        [Column("Logon_Date")]
        public DateTime LogonDate { get; set; }
        [Column("Is_Succesful")]
<<<<<<< HEAD
        public bool IsSuccesful { get; set; }

        [ForeignKey("Login")]
        public virtual SecurityLoginPoco? SecurityLogin { get; set; }
=======
        public Boolean IsSuccesful { get; set; }
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa

    }
}
