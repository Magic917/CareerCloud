﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        public object login;

        [Key]
        public Guid Id { set; get; }
        [Column("Login")]
        public Guid Login { get; set; }
        [Column("Role")]
        public Guid Role { get; set; }
        [Column("Time_Stamp")]
        public byte[]? TimeStamp { get; set; }

<<<<<<< HEAD
        public virtual SecurityLoginPoco? SecurityLogin { get; set; }
        public virtual SecurityRolePoco? SecurityRole { get; set; }

=======
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
