using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Roles")]
    public  class SecurityRolePoco : IPoco
    {
        [Key]
        public Guid Id { set; get; }
        [Column("Role")]
        public string Role { get; set; }
        [Column("Is_Inactive")]
<<<<<<< HEAD
        public bool  IsInactive { get; set; }
        public virtual ICollection<SecurityLoginsRolePoco>? SecurityLoginsRoles { get; set; }
=======
        public Boolean  IsInactive { get; set; }
>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
