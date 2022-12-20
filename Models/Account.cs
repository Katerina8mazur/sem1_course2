 using HttpServer_1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Models
{
    internal class Account
    {
        public int Id { get; set; }

        [DBField("login")]
        public string Login { get; set; }

        [DBField("password")]
        public string Password { get; set; }

        [DBField("account_name")]
        public string Name { get; set; }

        [DBField("avatar")]
        public string Avatar { get; set; }


        public Account(int id, string login, string password, string name, string avatar)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Avatar = avatar;
        }

        public Account()
        {
        }
    }
}
