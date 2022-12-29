using HttpServer_1.Attributes;
using HttpServer_1.Models;
using HttpServer_1.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Controllers
{
    [HttpController("categories")]
    internal class Categories: Controller
    {
        private static CategoryDAO categoryDAO = new CategoryDAO(connectionString);
        public static CategoryDAO DAO { get => categoryDAO; }

        [HttpGET(@"^\d+$")]
        [NeedAccountId]
        public MethodResponse ShowCategory(int categoryId, int accountId)
            => new MethodResponse(new View("main.html", new
            {
                IsAuthorized = accountId >= 0,
                Categories = categoryDAO.GetAll(),
                SelectedCategoryId = categoryId,
                Recipes = Recipes.DAO.GetAll().Where(r => r.CategoryId == categoryId).ToList(),
            }));
    }
}
