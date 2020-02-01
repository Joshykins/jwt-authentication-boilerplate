using System;
using System.Collections.Generic;

namespace mul.data
{
    public partial class Users
    {
        public Users()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public int AccountId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
