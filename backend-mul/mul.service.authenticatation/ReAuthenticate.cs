
using mul.data;
using mul.dtos;
using System;
using System.Linq;

namespace mul.service.authenticatation
{
    public class ReAuthenticator : ErrorDto
    {
        public AuthenticatedDto Authenticate(string inputToken)
        {
            //Verify token
            var tokenManager = new TokenManager();
            TokenContent tokenContent = tokenManager.VerifyToken(inputToken);

            //Check if verification worked
            if (tokenManager.Errored)
            {
                Errored = true;
                ErrorMessages.Add(tokenManager.ErrorMessage);
                return null;
            }

            //Refresh token
            string token = tokenManager.CreateToken(tokenContent.UserId);
            Authorization authorizationData;
            try
            {
                using (mulContext context = new mulContext())
                {
                    Authorizer authorizer = new Authorizer();
                    Users user = context.Users.FirstOrDefault(o => o.Id == tokenContent.UserId);
                    authorizationData = authorizer.GetAuthorization(context, user);
                }
            }
            catch (Exception Ex)
            {
                Errored = true;
                ErrorMessages.Add(Ex.Message);
                return null;
            }

            var output = new AuthenticatedDto
            {
                Token = token,
                Authorized = authorizationData
            };

            return output;


        }
    }
}
