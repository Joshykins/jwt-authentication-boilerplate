using mul.data;
using mul.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using JWT;
using System.Text;
using JWT.Builder;
using JWT.Algorithms;

namespace mul.service.authenticatation
{
    public class Authenticater : ErrorDto
    {
        public AuthenticatedDto Token { get; set; }
        public void AuthenticateSignin(string password, string email)
        {
            try
            {


                using (var context = new mulContext())
                {

                    //Find account via email
                    bool matchedWithEmail = context.Users.Any(o => o.Email == email.ToLower());
                    if (!matchedWithEmail)
                    {
                        Errored = true;
                        ErrorMessages.Add("Email does not exist for user.");
                        return;
                    }

                    var user = context.Users.FirstOrDefault(o => o.Email == email.ToLower());


                    //Verify password
                    if (BCrypt.Net.BCrypt.InterrogateHash(password).RawHash
                        != BCrypt.Net.BCrypt.InterrogateHash(user.Password).RawHash)
                    {
                        Errored = true;
                        ErrorMessages.Add("Passwords do not match.");
                        return;
                    }


                    var tokenManager = new TokenManager();
                    //Pull Authorization Data
                    var authorizer = new Authorizer();
                    Authorization authorizationData = authorizer.GetAuthorization(context, user);

                    //Generate Token
                    string token = tokenManager.CreateToken(user.Id);

                    //Create data transfer object
                    Token = new AuthenticatedDto
                    {
                        Token = token,
                        Authorized = authorizationData
                    };
                    //Signup done, send DTO back
                }
            }
            catch(Exception ex)
            {
                Errored = true;
                ErrorMessages.Add(ex.Message);
                return;
            }

        }
    }
}
