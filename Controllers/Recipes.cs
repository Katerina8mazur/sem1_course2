using HttpServer_1.Attributes;
using HttpServer_1.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Controllers
{
    [HttpController("recipes")]
    internal class Recipes : Controller
    {
        private static RecipeDAO recipeDAO = new RecipeDAO(connectionString);
        public static RecipeDAO DAO { get => recipeDAO; }

        [HttpGET(@"^$")]
        [NeedAccountId]
        public MethodResponse ShowMainPage(int accountId)
            => new MethodResponse(new View("main.html", new { 
                IsAuthorized = accountId >= 0,
                Categories = Categories.DAO.GetAll(),
                Recipes = recipeDAO.GetAll(),
            }));

        [HttpGET(@"^\d+$")]
        [NeedAccountId]
        public MethodResponse ShowRecipe(int id, int currentAccountId)
        {
            var recipe = recipeDAO.Get(id);
            if (recipe == null)
                throw new ServerException(System.Net.HttpStatusCode.NotFound);
            return new MethodResponse(new View("recipe.html", new
            {
                CurrentAccount = Accounts.DAO.Get(currentAccountId),
                Recipe = recipe,
            }));
        }
    }
}
