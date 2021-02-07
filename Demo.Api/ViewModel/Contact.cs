using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.ViewModel
{
    public class Contact
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Company Company { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public Address Address { get; set; }
        public string ImageFileUrl { get; set; }
    }
}
