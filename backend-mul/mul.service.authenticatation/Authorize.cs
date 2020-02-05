using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mul.data;
using mul.dtos;

namespace mul.service.authenticatation
{
    public class Authorizer
    {
        public Authorization GetAuthorization(mulContext context, Users user)
        {
            Authorization data = new Authorization
            {
                Owner = context.Accounts.FirstOrDefault(o => o.Id == user.AccountId).OwnerId == user.Id
            };

            return data;
        }
    }
}
