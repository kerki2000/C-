using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class BankAppDBContext : DbContext
    {
       
            public DbSet<Account> Accounts { get; set; }
            public DbSet<User> Users { get; set; }
        
    }
}
