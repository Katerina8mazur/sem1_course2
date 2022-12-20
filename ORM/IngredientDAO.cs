using HttpServer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.ORM
{
    internal class IngredientDAO
    {
        private MyORM orm;
        public IngredientDAO(string connectionString)
        {
            orm = new MyORM(connectionString, "Ingredients");
        }

        public List<Ingredient> GetAll()
            => orm.Select<Ingredient>();

        public Ingredient? Get(int id)
            => orm.Select<Ingredient>(id);
    }
}
