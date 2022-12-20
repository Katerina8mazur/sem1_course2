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
    }
}
