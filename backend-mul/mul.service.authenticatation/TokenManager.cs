using JWT;
using JWT.Algorithms;
using JWT.Builder;
using mul.dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace mul.service.authenticatation
{
    public class TokenManager
    {
        public string ErrorMessage { get; set; }
        
        private string secret { get; set; } = "Joshykins";
        public string CreateToken(int userId)
        {
            //Generate Token
            string token = new JwtBuilder()
               .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(secret)
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(336).ToUnixTimeSeconds())
                    .AddClaim("userid", userId)
                    .Build();

            return token;
        }

        public bool VerifyToken(string token)
        {
            try
            {
                var response = new JwtBuilder()
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode(token);
            }
            catch (TokenExpiredException)
            {
                ErrorMessage = "Token has expired";
                return false;
            }
            catch (SignatureVerificationException)
            {
                ErrorMessage = "Token has invalid signature";
                return false;
            }
            return true;
        }
    }
}
