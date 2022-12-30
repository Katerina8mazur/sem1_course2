using HttpServer_1.Attributes;
using HttpServer_1.Models;
using HttpServer_1.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static HttpServer_1.Models.Recipe;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGET("^new$")]
        [OnlyForAuthorized]
        public MethodResponse ShowRecipeCreatingPage()
            => new MethodResponse(new View("new-recipe.html", new { Categories = Categories.DAO.GetAll() }));

        [HttpPOST("^$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse CreateRecipe(string name, int categoryId, string ingredients, string text, int currentId)
        {
            try
            {
                GetParagraphes(text);
                var newRecipeId = recipeDAO.Insert(name, categoryId, currentId, text, ingredients);
                return new MethodResponse($"/recipes/{newRecipeId}");
            }
            catch
            {
                return new MethodResponse(new View("new-recipe.html", new 
                { 
                    Incorrect = true,
                    InputName = name,
                    InputCategoryId = categoryId,
                    InputIngredients = ingredients,
                    InputText = text,
                }));
            }
        }

        [HttpGET("^edit$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse ShowRecipeEditingPage(string _, int recipeId, int currentId)
        {
            var recipe = recipeDAO.Get(recipeId);
            if (recipe.AuthorId != currentId)
                throw new ServerException(System.Net.HttpStatusCode.Forbidden);
            return new MethodResponse(new View("edit-recipe.html", new
            {
                Recipe = recipe,
                Categories = Categories.DAO.GetAll()
            }));
        }

        [HttpPOST("^edit$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse EditRecipe(int recipeId, string name, int categoryId, string ingredients, string text, int currentId)
        {
            var recipe = recipeDAO.Get(recipeId);
            if (recipe.AuthorId != currentId)
                throw new ServerException(System.Net.HttpStatusCode.Forbidden);
            try
            {
                GetParagraphes(text);
                recipeDAO.Edit(recipeId, name, categoryId, text, ingredients);
                return new MethodResponse($"/recipes/{recipeId}");
            }
            catch
            {
                return new MethodResponse(new View("edit-recipe.html", new
                {
                    Recipe = recipe,
                    Categories = Categories.DAO.GetAll(),
                    Incorrect = true,
                    InputName = name,
                    InputCategoryId = categoryId,
                    InputIngredients = ingredients,
                    InputText = text,
                }));
            }
        }

        [HttpPOST("^delete$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse DeleteRecipe(int recipeId, int currentId)
        {
            var recipe = recipeDAO.Get(recipeId);
            if (recipe.AuthorId != currentId)
                throw new ServerException(System.Net.HttpStatusCode.Forbidden);

            recipeDAO.Delete(recipeId);
            return new MethodResponse($"/recipes");
        }
    }
}
