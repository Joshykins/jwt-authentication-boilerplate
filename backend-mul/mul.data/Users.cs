using System;
using System.Collections.Generic;

namespace mul.data
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateCreated { get; set; }
        public int AccountId { get; set; }

        public Accounts Account { get; set; }
    }
}
