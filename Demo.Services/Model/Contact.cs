using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.Model
{
    public class Contact
    {
        public int UserID { get; set; }
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
