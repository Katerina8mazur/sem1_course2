﻿using HttpServer_1.Attributes;
using HttpServer_1.Models;
using HttpServer_1.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpServer_1.Controllers
{
    [HttpController("accounts")]
    internal class Accounts : Controller
    {
        private static AccountDAO accountDAO = new AccountDAO(connectionString);

        [HttpGET(@"^login$")]
        public MethodResponse ShowAuthorizationPage() 
            => new MethodResponse(new View("/login.html"));

        [HttpGET(@"^register$")]
        public MethodResponse ShowRegistrationPage()
            => new MethodResponse(new View("/register.html"));

        [HttpPOST("^login$")]
        public MethodResponse Login(string login, string password)
        {
            var accountId = accountDAO.Check(HttpUtility.UrlDecode(login), HttpUtility.UrlDecode(password));

            if (accountId >= 0)
            {
                var session = SessionManager.CreateSession(accountId, login);
                Cookie? cookie = new Cookie("SessionId", session.Id.ToString(), "/");
                return new MethodResponse("/recipes", cookie);
            }

            return new MethodResponse(new View("/login.html", new { Incorrect = true, InputLogin = HttpUtility.UrlDecode(login) }));
        }

        [HttpPOST("^register$")]
        public MethodResponse Register(string login, string name, string password, string passwordConfirm)
        {
            if (password != passwordConfirm)
                return new MethodResponse(new View("/register.html", new { Error = "incorrect_password", 
                    InputLogin = HttpUtility.UrlDecode(login),
                    InputName = HttpUtility.UrlDecode(name)
                }));
            if (!accountDAO.IsLoginAvailable(login))
                return new MethodResponse(new View("/register.html", new { Error = "existing_login", 
                    InputLogin = HttpUtility.UrlDecode(login),
                    InputName = HttpUtility.UrlDecode(name)
                }));

            accountDAO.Insert(HttpUtility.UrlDecode(login), HttpUtility.UrlDecode(name), HttpUtility.UrlDecode(password));
            return Login(login, password);
        }

        [HttpGET("^my$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse ShowCurrentAccount(string _, int id)
            => ShowAccountById(id, id);

        [HttpGET(@"^\d+$")]
        [NeedAccountId]
        public MethodResponse ShowAccountById(int id, int currentAccountId)
        {
            var account = accountDAO.Get(id);
            if (account == null)
                throw new ServerException(HttpStatusCode.NotFound);
            return new MethodResponse(new View("/profile.html", new { Profile = account, CurrentAccountId = currentAccountId }));
        }

        //[HttpGET("^$")]
        //[OnlyForAuthorized]
        //public MethodResponse<List<Account>>  GetAccounts()
        //    => new MethodResponse<List<Account>>(accountDAO.GetAll());

        //[HttpPOST("^save$")]
        //public MethodResponse SaveAccount(string login, string password)
        //{
        //    accountDAO.Insert(login, password);
        //    return new MethodResponse();
        //}

        ////Get /accounts/ - список аккаунтов в формате json
        ////Get /accounts/{id} - информация об одном аккаунте
        ////Post /accounts/ - добавлять инф на сервер - принимает параметры через body

        ////надо брать все данные из бд




    }
}
