using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Enums
{
    public class Address : ValueObject
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
