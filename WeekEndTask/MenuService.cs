using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekEndTask
{
    internal class MenuService
    {
        AccountService accountService;
        BankService bankService;
        public MenuService(AccountService accountService,BankService bankService)
        {
            this.accountService = accountService;
            this.bankService = bankService;
        }
        public void ShowMenu()
        {
            Console.WriteLine("1.User Registration");
            Console.WriteLine("2.User Login");
            Console.WriteLine("3.Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterUser();
                    break;
                case "2":
                    LoginUser();
                    break;
                case "3":
                    Console.WriteLine("Proqramdan çıxırsınız.");
                    return;
                default:
                    Console.WriteLine("Yanlış seçim etdiniz, yenidən cəhd edin.");
                    ShowMenu();
                    break;
            }
        }

        public void RegisterUser()
        {
            Console.WriteLine("Adi daxil edin:");
            string name = Console.ReadLine();
            if (name.Length < 3)
            {
                Console.WriteLine("Ad 3 simvoldan çox olmalıdır.");
                RegisterUser();
                return;
            }

            Console.WriteLine("Soyadi daxil edin:");
            string surname = Console.ReadLine();
            if (surname.Length < 3)
            {
                Console.WriteLine("Soyad 3 simvoldan çox olmalıdır.");
                RegisterUser();
                return;
            }

            Console.WriteLine("Parolu daxil edin:");
            string password = Console.ReadLine();
            if (password.Length < 8 || !password.Any(char.IsLower) || !password.Any(char.IsUpper))
            {
                Console.WriteLine("Parol minimum 8 simvol olmalı, bir böyük və bir kiçik hərf olmalıdır.");
                RegisterUser();
                return;
            }

            Console.WriteLine("Balansı daxil edin:");
            double balance = Convert.ToDouble(Console.ReadLine());
            if (balance <= 0)
            {
                Console.WriteLine("Balans 0-dan böyük olmalıdır.");
                RegisterUser();
                return;
            }

            Console.WriteLine("Emaili daxil edin:");
            string email = Console.ReadLine();
            if (accountService.FindUserByEmail(email) != null)
            {
                Console.WriteLine("Bu email artıq istifadə olunur.");
                RegisterUser();
                return;
            }

            accountService.Register(name, surname, password, balance, email);
            Console.WriteLine("Qeydiyyat tamamlandı.");
            ShowMenu();
        }

        public void LoginUser()
        {
            Console.WriteLine("Emaili daxil edin:");
            string email = Console.ReadLine();

            Console.WriteLine("Parolu daxil edin:");
            string password = Console.ReadLine();

            accountService.Login(email, password);

            if (accountService.currentUser != null)
            {
                if (accountService.currentUser.IsAdmin)
                    ShowAdminMenu();
                else
                    ShowUserMenu();
            }
            else
            {
                Console.WriteLine("Daxilolma uğursuz oldu.");
                ShowMenu();
            }
        }
        public void ShowUserMenu()
        {
            if (accountService.currentUser == null)
            {
                Console.WriteLine("Zəhmət olmasa əvvəlcə login olun.");
                ShowMenu(); 
                return;
            }
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Top up balance");
            Console.WriteLine("3. Change Password");
            Console.WriteLine("4. Log out");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bankService.CheckBalance(accountService.currentUser);
                    break;
                case "2":
                    Console.WriteLine("Meblegi daxil edin");
                    double amount = Convert.ToDouble(Console.ReadLine());
                    bankService.Deposit(accountService.currentUser,amount);
                    break;
                case "3":
                    Console.WriteLine("Old password");
                    string oldPassword = Console.ReadLine();
                    Console.WriteLine("New password");
                    string newPassword = Console.ReadLine();
                    accountService.ChangePassword(accountService.currentUser.Email,oldPassword,newPassword);
                    ShowUserMenu();
                    break;
                case "4":
                    accountService.LogOut(accountService.currentUser.Email);
                    ShowMenu();
                    break;

                default:
                    Console.WriteLine("Invalid option, try again.");
                    ShowUserMenu();
                    break;
            }
        }
        public void ShowAdminMenu()
        {
            Console.WriteLine("1. View user list");
            Console.WriteLine("2. Block User");
            Console.WriteLine("3. Log out");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bankService.BankUserList();
                    break;
                case "2":
                    Console.WriteLine("Emaili daxil edin");
                    string email = Console.ReadLine();
                    bankService.BlockUser(email);
                    break;
                case "3":
                    accountService.LogOut(accountService.currentUser.Email);
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    ShowAdminMenu();
                    break;
            }
        }

    }
}
