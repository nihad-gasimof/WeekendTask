using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WeekEndTask
{
    internal class AccountService
    {
        private List<User> _users;
        public User currentUser { get; private set; }
        public AccountService(List<User> users)
        {
            _users = users;
        }
        public void Register(string name, string surname, string password, double balance, string email, bool isAdmin = false)
        {
            if (_users.Any(u => u.Email == email))
            {
                Console.WriteLine("Bu user artiq var");
                return;
            }
            User newUser = new User(name, surname, password, balance, email, isAdmin, false, false);
            _users.Add(newUser);
            Console.WriteLine($"User {name} {surname} qeydiyyati tamamladi.");
        }
        public void Login(string email, string password)
        {
            User user = FindUserByEmail(email);

            if (user == null)
            {
                Console.WriteLine("Bu emailə aid istifadəçi tapılmadı.");
                return;
            }

            if (user.Password != password)
            {
                Console.WriteLine("Şifrə səhvdir.");
                return;
            }

            if (user.IsBlocked)
            {
                Console.WriteLine("Bu istifadəçi bloklanıb.");
                return;
            }

            currentUser = user;
            user.IsLogged = true;
            Console.WriteLine($"User {user.Name} {user.Surname} login oldu.");
        }


        
        public User FindUserByEmail(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Email bos ola bilmez");
                return null;
            }
            return _users.FirstOrDefault(u => u.Email == email);
        }
        public void LogOut(string email)
        {
            _users.ForEach(user =>
            {
                if (user != null)
                {

                    if (user.Email == email)
                    {
                        user.IsLogged = false;
                        Console.WriteLine($"User {user.Name} {user.Surname} logout.");
                    }
                }
                else
                {
                    Console.WriteLine("User null ola bilmez");
                }
            });

        }
        public void ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = FindUserByEmail(email);
            if (user == null)
            {
                Console.WriteLine("User tapilmadi");
                return;
            }
            if (user.Password != oldPassword)
            {
                Console.WriteLine("Kohne sifre sehvdir");
                return;
            }
            user.Password = newPassword;
            Console.WriteLine($"User {user.Name} {user.Surname} sifresi deyisdirildi.");
        }
    }
}
