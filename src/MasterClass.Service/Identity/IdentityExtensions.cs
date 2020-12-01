using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MasterClass.Repository.Models.Users;

namespace MasterClass.Service.Identity
{
    public static class IdentityExtensions
    {
        private const string JWTROLE_CLAIMNAME = "role";

        public static IEnumerable<Claim> GetJwtClaims(this User user, DateTimeOffset issuedAt)
        {
            foreach (var role in user.Roles)
            {
                yield return new Claim(JWTROLE_CLAIMNAME, role);
            }

            yield return new Claim(JwtRegisteredClaimNames.Iat, issuedAt.ToUnixTimeSeconds().ToString());
            yield return new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString());
            yield return new Claim(JwtRegisteredClaimNames.Sub, user.Login);
            yield return new Claim(JwtRegisteredClaimNames.GivenName, user.Name);
            yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        }
    }
}