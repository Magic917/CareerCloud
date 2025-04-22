using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco : IPoco
    {
        
        public Guid Id { set; get; }
        [Column("LanguageID")]
        [Key]
        public string LanguageID { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Native_Name")]
        public string NativeName { get; set; }
<<<<<<< HEAD
        public virtual ICollection<CompanyDescriptionPoco>? CompanyDescriptions { get; set; }
=======

>>>>>>> 110d889c1008a037268288b9d2b7578fa2b05cfa
    }
}
