using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
   public class BankAppViewModel
    {
        BankAppDBContext context = new BankAppDBContext();

        public List<User> GetAllUsersData()
        {
            return context.Users.Include("UserAccount").ToList();
           
        }

            public void AddNewUser(User newUser)
        {
            context.Users.Add(newUser);
            context.SaveChanges();
        }
        public void UpdateUser(User updated)
        {
            User current = (from u in context.Users
                            where u.ID == updated.ID
                                select u).SingleOrDefault();

            current.Name = updated.Name;
            current.Password = updated.Password;
           
            context.SaveChanges();
        }
        public void DepositBalance(User newUser,double deposit)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == newUser.ID
                         select u).SingleOrDefault();
            user.UserAccount.Balance += deposit;

            context.SaveChanges();
        }
        public void WithDrawBalance(User newUser, double withdraw)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == newUser.ID
                         select u).SingleOrDefault();
            user.UserAccount.Balance -= withdraw;

            context.SaveChanges();
        }
        public Account  GetAccountPerUser(int ID)
        {
            return (from e in context.Users
                    where e.ID == ID
                    select e.UserAccount).SingleOrDefault();
        }
        public void DeleteUserRecord(int ID)
        {
            User toBeDeleted = (from u in context.Users
                                where u.ID == ID
                                    select u).SingleOrDefault();

            Account  AccountToBeDeleted = GetAccountPerUser(ID);
             context.Accounts.Remove(AccountToBeDeleted);
            context.Users.Remove(toBeDeleted);
            context.SaveChanges();
        }
        public User Login(int logId,string logPassword)
       {
           
           User user = (from u in context.Users.Include("UserAccount")
                    where u.ID == logId && u.Password.Contains(logPassword)
                    select u).SingleOrDefault();
            

            return user;

            
        }
        public User BalanceById(int logId)
        {

            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == logId 
                         select u).SingleOrDefault();


            return user;


        }
        public double GetBalance(User user)
        {
            return (from u in context.Users.Include("UserAccount")
                    where u.ID == user.ID
                    select u.UserAccount.Balance).SingleOrDefault();

        }
        public void DepositTransactionToLogFile(User newUser,string fileName,double deposit)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == newUser.ID
                         select u).SingleOrDefault();

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(fileName, true))
                {

                  
                        streamWriter.WriteLine(DateTime.Now + "|" +"#"+ user.UserAccount.AccountNumber + "|" + "Deposit" + "|"
                            + user.UserAccount.Balance.ToString("c2") + "|" + (user.UserAccount.Balance+deposit).ToString("c2") );
                   


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
        public void WithDrawTransactionToLogFile(User newUser, string fileName, double withdraw)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == newUser.ID
                         select u).SingleOrDefault();

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(fileName, true))
                {


                    streamWriter.WriteLine(DateTime.Now + "|" + "#" + user.UserAccount.AccountNumber + "|" + "Withdraw" + "|"
                        + user.UserAccount.Balance.ToString("c2") + "|" + (user.UserAccount.Balance + withdraw).ToString("c2"));



                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
    }
   
}
