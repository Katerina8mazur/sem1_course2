using HttpServer_1.Attributes;
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
        public static AccountDAO DAO { get => accountDAO; }

        [HttpGET(@"^login$")]
        public MethodResponse ShowAuthorizationPage() 
            => new MethodResponse(new View("login.html"));

        [HttpGET(@"^register$")]
        public MethodResponse ShowRegistrationPage()
            => new MethodResponse(new View("register.html"));

        [HttpPOST("^login$")]
        public MethodResponse Login(string login, string password, bool rememberMe)
        {
            var accountId = accountDAO.Check(login, password);

            if (accountId >= 0)
            {
                var session = SessionManager.CreateSession(accountId, rememberMe);
                Cookie? cookie = new Cookie("SessionId", session.Id.ToString(), "/");
                return new MethodResponse("/recipes", cookie);
            }

            return new MethodResponse(new View("login.html", new { Incorrect = true, InputLogin = login }));
        }

        [HttpPOST("^register$")]
        public MethodResponse Register(string login, string name, string password, string passwordConfirm, bool rememberMe)
        {
            if (password != passwordConfirm)
                return new MethodResponse(new View("register.html", new { Error = "incorrect_password", 
                    InputLogin = login,
                    InputName = name
                }));
            if (!accountDAO.IsLoginAvailable(login))
                return new MethodResponse(new View("register.html", new { Error = "existing_login", 
                    InputLogin = login,
                    InputName = name
                }));

            accountDAO.Insert(login, name, password);
            return Login(login, password, rememberMe);
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
            return new MethodResponse(new View("profile.html", new { Profile = account, CurrentAccountId = currentAccountId }));
        }

        [HttpPOST("^edit$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse EditMyPage(string login, string name, string password, int id)
        {
            var account = accountDAO.Get(id);
            if (account.Login != login && login != "" && accountDAO.IsLoginAvailable(login))
                accountDAO.ChangeLogin(id, login);
            if (account.Name != name && name != "")
                accountDAO.ChangeName(id, name);
            if (account.Password != password && password != "")
                accountDAO.ChangePassword(id, password);

            return new MethodResponse("/accounts/my");
        }

        [HttpPOST("^logout$")]
        [OnlyForAuthorized]
        [NeedSessionId]
        public MethodResponse Logout(string _, Guid sessionId)
        {
            SessionManager.DeleteSession(sessionId);
            return new MethodResponse("/recipes");
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
