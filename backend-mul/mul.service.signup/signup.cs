using mul.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using mul.data;

namespace mul.service.signup
{
    public class Signup : ErrorDto
    {
        public void SignupAccountAndUser(RegisterDto registration)
        {

            //Encrypt Password
            registration.Password = BCrypt.Net.BCrypt.HashPassword(registration.Password);
            //.verify for checking
            var time = DateTime.UtcNow;

            var account = new Accounts
            {
                AccountName = registration.AccountName,
                DateCreated = time,
                Email = registration.Email
            };
            var user = new Users
            {
                Account = account,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                DateCreated = time,
                Email = registration.Email,
                Password = registration.Password
            };

            using (var context = new mulContext())
            {

                bool matchedAccount = context.Accounts.Any(o => o.Email == registration.Email);
                bool matchedUser = context.Users.Any(o => o.Email == registration.Email);
                if (matchedAccount || matchedUser) {
                    if (matchedAccount)
                    {
                        Errored = true;
                        ErrorMessages.Add("Email already exists for an account");
                        //debug
                        context.Accounts.RemoveRange(context.Accounts.Where(o => o.Email == registration.Email));
                        context.SaveChanges();

                    }
                    if (matchedUser)
                    {
                        Errored = true;
                        ErrorMessages.Add("Email already exists for a user");
                    }

                }
                else
                {
                    context.Users.Add(user);
                }
                context.SaveChanges();
                account.Owner = user;
                context.SaveChanges();
                //Check if exists based on email

                ErrorMessages.Add("successfully added new account");
            }




        }


    }
}
