using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace mul.dtos
{
    public class SigninDto : ErrorDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    static public class SanitizeSigninDto
    {
        public static SigninDto Sanitize(SigninDto signinDto)
        {
            //Check nulls
            // No more 255
            signinDto = AllFieldsFilled(signinDto);
            //Email passes regex
            signinDto = ValidEmail(signinDto);
            //Regex
            //must container lowercase
            //must container uppercase
            //must be 8 characters long
            //Not more than 255 char'
            signinDto = ValidPassword(signinDto);
            return signinDto;
        }
        private static SigninDto AllFieldsFilled(SigninDto signninDto)
        {
            
            if (signninDto.Password == null || signninDto.Password == "")
            {
                signninDto.Errored = true;
                signninDto.ErrorMessages.Add("Password can't be left empty");
            }
            
            if (signninDto.Email == null || signninDto.Email == "")
            {
                signninDto.Errored = true;
                signninDto.ErrorMessages.Add("Email can't be left empty");
            }

            return signninDto;
        }


        private static SigninDto ValidPassword(SigninDto signinDto)
        {
            Regex mustContainLowercase = new Regex(@"^(?=.*[a-z])");
            Regex mustContainUppercase = new Regex(@"^(?=.*[A-Z])");
            Regex mustContainNumber = new Regex(@"[0-9]+");

            if (!mustContainLowercase.Match(signinDto.Password).Success)
            {
                signinDto.Errored = true;
                signinDto.ErrorMessages.Add("Password must contain a lowercase character.");
            }
            if (!mustContainUppercase.Match(signinDto.Password).Success)
            {
                signinDto.Errored = true;
                signinDto.ErrorMessages.Add("Password must contain a uppercase character.");
            }
            if (!mustContainNumber.Match(signinDto.Password).Success)
            {
                signinDto.Errored = true;
                signinDto.ErrorMessages.Add("Password must contain a number.");
            }
            if (signinDto.Password.Length < 8)
            {
                signinDto.Errored = true;
                signinDto.ErrorMessages.Add("Password must be at least 8 characters long.");
            }
            return signinDto;
        }


        private static SigninDto ValidEmail(SigninDto signinDto)
        {
            try
            {
                MailAddress addr = new MailAddress(signinDto.Email);
                if (addr.Address != signinDto.Email)
                {
                    signinDto.Errored = true;
                    signinDto.ErrorMessages.Add("Invalid Email Address.");
                }
            }
            catch
            {
                signinDto.Errored = true;
                signinDto.ErrorMessages.Add("Invalid Email Address.");
            }

            //Check if too longer for datatype.
            if (signinDto.Email.Length > 255)
            {
                signinDto.Errored = true;
                signinDto.ErrorMessages.Add("Email was too long.");
            }

            return signinDto;

        }
    }
}
