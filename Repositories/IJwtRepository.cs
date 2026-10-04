using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace dev_habit.Repositories;

public interface IJwtRepository
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}