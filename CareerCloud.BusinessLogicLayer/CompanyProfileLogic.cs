﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            string[] requiredExtendedWebSite = new string[] { ".ca", ".com", ".biz" };
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty( poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, $"CompanyWebsite for CompanyProfilePoco {poco.Id} must contain an extended character of '.ca', '.com', '.biz'."));
                }
                else if (!requiredExtendedWebSite.Any(t => poco.CompanyWebsite.EndsWith(t)))
                {
                    exceptions.Add(new ValidationException(600, $"CompanyWebsite for CompanyProfilePoco {poco.Id} must contain an extended character of '.ca', '.com', '.biz'."));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfilePoco {poco.Id} must follow patern"));
                }
                else if (!Regex.IsMatch(poco.ContactPhone, @"\A[0-9]{3}-\d{3}-\d{4}"))
                {
                    exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfilePoco {poco.Id} must follow patern"));
                }

                //else
                //{
                //    string[] phoneComponents = poco.ContactPhone.Split('-');
                //    if (phoneComponents.Length != 3)
                //    {
                //        exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                //    }
                //    else
                //    {
                //        if (phoneComponents[0].Length != 3)
                //        {
                //            exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                //        }
                //        else if (phoneComponents[1].Length != 3)
                //        {
                //            exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                //        }
                //        else if (phoneComponents[2].Length != 4)
                //        {
                //            exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                //        }
                //    }

                //}


            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
