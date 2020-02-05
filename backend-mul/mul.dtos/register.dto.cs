using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace mul.dtos
{
    public class RegisterDto : ErrorDto
    {
        //Posted to server when you register
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    static public class SanitizeRegisterDto
    {
        public static RegisterDto Sanitize(RegisterDto registerDto)
        {
            //Checks if all are not null
            //TODO: Compare against records
            registerDto = AllFieldsFilled(registerDto);
            if (registerDto.Errored)
            {
                //Escape here due to potential nulls
                return registerDto;
            }

            //Checks if is valid email
            //Not more than 255 char
            registerDto = ValidEmail(registerDto);
            //Not more than 255 char 
            registerDto = ValidFirstname(registerDto);
            //Not more than 255 char 
            registerDto = ValidLastname(registerDto);
            //Regex
            //must container lowercase
            //must container uppercase
            //must be 8 characters long
            //Not more than 255 char
            registerDto = ValidPassword(registerDto);

            return registerDto;
        }

        private static RegisterDto AllFieldsFilled(RegisterDto registerDto)
        {
            if (registerDto.FirstName == null || registerDto.FirstName == "")
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Firstname can't be be left empty.");
            }
            if (registerDto.LastName == null || registerDto.LastName == "")
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Lastname can't be be left empty.");
            }
            //Phoennumber can be null
            if (registerDto.Password == null || registerDto.Password == "")
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Password can't be left empty");
            }
            if(registerDto.Email == null || registerDto.Email == "")
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Email can't be left empty");;
            }

            return registerDto;
        }


        private static RegisterDto ValidPassword(RegisterDto registerDto)
        {
            Regex mustContainLowercase = new Regex(@"^(?=.*[a-z])");
            Regex mustContainUppercase = new Regex(@"^(?=.*[A-Z])");
            Regex mustContainNumber = new Regex(@"[0-9]+");

            if (!mustContainLowercase.Match(registerDto.Password).Success)
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Password must contain a lowercase character.");
            }
            if (!mustContainUppercase.Match(registerDto.Password).Success)
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Password must contain a uppercase character.");
            }
            if (!mustContainNumber.Match(registerDto.Password).Success)
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Password must contain a number.");
            }
            if (registerDto.Password.Length < 8)
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("Password must be at least 8 characters long.");
            }
            return registerDto;
        }

        private static RegisterDto ValidFirstname(RegisterDto registerDto)
        {
            if (registerDto.FirstName.Length > 255)
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("First Name Too Longh");
            }

            return registerDto;
        }


        private static RegisterDto ValidLastname(RegisterDto registerDto)
        {
            if (registerDto.LastName.Length > 255)
            {
                registerDto.Errored = true;
                registerDto.ErrorMessages.Add("First Name Too Longh");
            }

            return registerDto;
        }



        private static RegisterDto ValidEmail(RegisterDto RegisterDto)
        {
            try
            {
                MailAddress addr = new MailAddress(RegisterDto.Email);
                if (addr.Address != RegisterDto.Email)
                {
                    RegisterDto.Errored = true;
                    RegisterDto.ErrorMessages.Add("Invalid Email Address.");
                }
            }
            catch
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Invalid Email Address.");
            }

            //Check if too longer for datatype.
            if (RegisterDto.Email.Length > 255)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Email was too long.");
            }

            return RegisterDto;

        }
    }
}
