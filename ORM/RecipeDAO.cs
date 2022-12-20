using HttpServer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.ORM
{
    internal class RecipeDAO
    {
            private MyORM orm;
            public RecipeDAO(string connectionString)
            {
                orm = new MyORM(connectionString, "Recipes");
            }

            public List<Recipe> GetAll()
                => orm.Select<Recipe>();

            public Recipe? Get(int id)
                => orm.Select<Recipe>(id);
     }
}
