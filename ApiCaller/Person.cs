using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCaller
{
    public class Person
    {
        public Person()
        {
                
        }

        public Person(string creditorId,string firstName, string lastName, string nationalCode, string personId)
        {
            this.CreditorId = creditorId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.NationalCode = nationalCode;
            this.PersonId = personId;
        }

        public string CreditorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalCode { get; set; }

        public string PersonId { get; set; }

    }
}
