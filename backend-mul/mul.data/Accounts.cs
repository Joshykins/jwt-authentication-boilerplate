using System;
using System.Collections.Generic;

namespace mul.data
{
    public partial class Accounts
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public DateTime DateCreated { get; set; }
        public int? OwnerId { get; set; }

        public Accounts IdNavigation { get; set; }
        public Accounts InverseIdNavigation { get; set; }
        public Users Users { get; set; }
    }
}
