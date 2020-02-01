using System;
using System.Collections.Generic;

namespace mul.data
{
    public partial class Accounts
    {
        public Accounts()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Email { get; set; }
        public int? OwnerId { get; set; }

        public virtual Users Owner { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
