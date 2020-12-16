using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class AddressTableResponse
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string PropertyType { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int Zip { get; set; }
    }
}
