using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.Model
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int Expires { get; set; }
        public string SharedUserName { get; set; }
        public string SharedPassword { get; set; }
    }
}
