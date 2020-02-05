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
    public class Authenticator : ErrorDto
    {
        public AuthenticatedDto Token { get; set; }
        public void AuthenticateSignin(string password, string email)
        {
            
            using ( var context = new mulContext() )
            {
                bool matchedWithEmail = context.Users.Any(o => o.Email == email.ToLower());
                if(!matchedWithEmail)
                {
                    Errored = true;
                    ErrorMessages.Add("Email does not exist for user.");
                    return;
                }

                var user = context.Users.FirstOrDefault(o => o.Email == email.ToLower());


                if(BCrypt.Net.BCrypt.InterrogateHash(password).RawHash 
                    != BCrypt.Net.BCrypt.InterrogateHash(user.Password).RawHash)
                {
                    Errored = true;
                    ErrorMessages.Add("Passwords do not match.");
                    return;
                }


                var tokenManager = new TokenManager();

                //Get authorization Data
                bool owner = context.Accounts.FirstOrDefault(o=>o.Id == user.AccountId).OwnerId == user.Id;
                string token = tokenManager.CreateToken(user.Id);
                

                Token = new AuthenticatedDto
                {
                    Token = token,
                    Authorized = new Authorization
                    {
                        Owner = owner
                    }
                };

            }
            //Find account via email
            //Verify password
            //Pull Authentication Data
            //Pull Authorization Data
            //Generate Token


        }
    }
}
