using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
   public class User
    {    
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name{ get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        
        public Account UserAccount { get; set; }
    }
}
