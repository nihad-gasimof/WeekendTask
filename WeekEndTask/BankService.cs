using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekEndTask
{
    internal class BankService
    {

        public void CheckBalance(User user)
        {
                if(user.IsLogged && !user.IsBlocked)
                {
                    Console.WriteLine($"User {user.Name} {user.Surname} has a balance of {user.Balance}");
                }
        }
        public void Deposit(User user, double amount)
        {
                if(user.IsLogged && !user.IsBlocked)
                {
                    user.Balance += amount;
                    Console.WriteLine($"User {user.Name} {user.Surname} deposited {amount}. New balance is {user.Balance}");
                }
        }
        public void BankUserList()
        {
            foreach(var user in Bank.Users)
            {
                Console.WriteLine($"User ID: {user.Id}, Name: {user.Name} {user.Surname}, Balance: {user.Balance}, Email: {user.Email}, IsAdmin: {user.IsAdmin}, IsBlocked: {user.IsBlocked}, IsLogged: {user.IsLogged}");
            }
        }
      public void BlockUser(string email)
        {
             foreach(var user in Bank.Users)
             {
                if(user.Email == email)
                {
                    user.IsBlocked = true;
                    Console.WriteLine($"User {user.Name} {user.Surname} has been blocked.");
                }
            }
        }

    }
}
