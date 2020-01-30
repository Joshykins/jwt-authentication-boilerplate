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
        public static RegisterDto Sanitize(RegisterDto RegisterDto)
        {
            //Checks if all are not null
            //TODO: Compare against records
            RegisterDto = AllFieldsFilled(RegisterDto);
            if (RegisterDto.Errored)
            {
                //Escape here due to potential nulls
                return RegisterDto;
            }

            //Checks if is valid email
            //Not more than 255 char
            RegisterDto = ValidEmail(RegisterDto);
            //Not more than 255 char 
            RegisterDto = ValidFirstname(RegisterDto);
            //Not more than 255 char 
            RegisterDto = ValidLastname(RegisterDto);
            //Regex
            //must container lowercase
            //must container uppercase
            //must be 8 characters long
            //Not more than 255 char
            RegisterDto = ValidPassword(RegisterDto);

            return RegisterDto;
        }

        private static RegisterDto AllFieldsFilled(RegisterDto RegisterDto)
        {
            if (RegisterDto.FirstName == null || RegisterDto.FirstName == "")
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Firstname can't be be left empty.");
            }
            if (RegisterDto.LastName == null || RegisterDto.LastName == "")
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Lastname can't be be left empty.");
            }
            //Phoennumber can be null
            if (RegisterDto.Password == null || RegisterDto.Password == "")
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Password can't be left empty");
            }

            return RegisterDto;
        }


        private static RegisterDto ValidPassword(RegisterDto RegisterDto)
        {
            Regex mustContainLowercase = new Regex(@"^(?=.*[a-z])");
            Regex mustContainUppercase = new Regex(@"^(?=.*[A-Z])");
            Regex mustContainNumber = new Regex(@"[0-9]+");

            if (!mustContainLowercase.Match(RegisterDto.Password).Success)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Password must contain a lowercase character.");
            }
            if (!mustContainUppercase.Match(RegisterDto.Password).Success)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Password must contain a uppercase character.");
            }
            if (!mustContainNumber.Match(RegisterDto.Password).Success)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Password must contain a number.");
            }
            if (RegisterDto.Password.Length < 8)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("Password must be at least 8 characters long.");
            }
            return RegisterDto;
        }

        private static RegisterDto ValidFirstname(RegisterDto RegisterDto)
        {
            if (RegisterDto.FirstName.Length > 255)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("First Name Too Longh");
            }

            return RegisterDto;
        }


        private static RegisterDto ValidLastname(RegisterDto RegisterDto)
        {
            if (RegisterDto.LastName.Length > 255)
            {
                RegisterDto.Errored = true;
                RegisterDto.ErrorMessages.Add("First Name Too Longh");
            }

            return RegisterDto;
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
