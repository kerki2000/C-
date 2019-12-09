using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Models
{
   public class Account
    {   [Key]
        public int AccountNumber { get; set; }
        
        public double Balance { get; set; }
    }
}
