using ContactManager.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public class ContactsDbContext : DbContext
    {
        //configure database
        public ContactsDbContext() : base("defaultConnection")
        {

        }
        //configure table
        public DbSet<Contact> Contacts { get; set; }
    }
}
