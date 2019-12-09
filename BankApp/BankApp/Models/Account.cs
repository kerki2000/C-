﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
   public class Account
    {
        private int _accountNumber;
        private double _balance;
        [Key]
        public int AccountNumber {
            get {

                return _accountNumber;
            }
            set {
                _accountNumber = value;
            }
        }
       
        public double Balance
        {
            get
            {

                return _balance;
            }
            set
            {
                _balance = value;
            }
        }
    }
}
