using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekEndTask
{
    internal class User
    {
        public int Id { get; set; }
        private static int _idCounter = 1;
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Balance { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
        public bool IsLogged { get; set; } = false;



        public User(string Name, string Surname,string password, double Balance, string Email, bool IsAdmin, bool IsBlocked, bool isLogged)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Balance = Balance;
            this.Email = Email;
            this.Password = password;
            this.IsAdmin = IsAdmin;
            this.IsBlocked = IsBlocked;
            this.IsLogged = IsLogged;
            Id = _idCounter++;
        }
    }
}
