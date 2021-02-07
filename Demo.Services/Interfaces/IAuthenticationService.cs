using Demo.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string GetToken(JwtTokenClaims jwtTokenClaims);
    }
}
