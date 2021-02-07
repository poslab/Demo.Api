using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.Model
{
    public class JwtTokenClaims
    {
        public string isShared { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
