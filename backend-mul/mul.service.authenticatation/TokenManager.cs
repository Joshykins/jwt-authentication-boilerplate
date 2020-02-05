using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using mul.dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mul.service.authenticatation
{
    public class TokenContent
    {
        public DateTimeOffset ExpirationDate { get; set; }
        public int UserId { get; set; }
    }
    public class TokenManager
    {
        public string ErrorMessage { get; set; }
        public bool Errored { get; set; } = false;
        
        //TODO::Move to a config file, and make a proper secret lol
        private string Secret { get; set; } = "Joshykins";

        //Creates new tokens
        public string CreateToken(int userId)
        {
            string payload = JsonConvert.SerializeObject(new TokenContent
            {
                ExpirationDate = DateTimeOffset.UtcNow.AddHours(336),
                UserId = userId
            });

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, Secret);

            return token;
        }

        //validates incoming tokens
        public TokenContent VerifyToken(string token)
        {

            TokenContent output;

            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                string json = decoder.Decode(token, Secret, verify: true);
                output = JsonConvert.DeserializeObject<TokenContent>(json);
            }
            catch (TokenExpiredException)
            {
                ErrorMessage = "Token has expired";
                Errored = true;
                return null;
            }
            catch (SignatureVerificationException)
            {
                ErrorMessage = "Token has invalid signature";
                Errored = true;
                return null;
            }

            return output;
        }
    }
}
