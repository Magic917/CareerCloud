using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco : IPoco
    {
        [Key]
        public string Code 
        {

            get;
            set; 
        }
        [Column("Name")]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public virtual ICollection<ApplicantProfilePoco>? ApplicantProfile { get; set; }

        public virtual ICollection<ApplicantWorkHistoryPoco>? ApplicantWorkHistory { get; set; }
        public virtual ICollection<CompanyLocationPoco>? CompanyLocations { get; set; }
    }
}
