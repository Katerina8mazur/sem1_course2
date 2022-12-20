using HttpServer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.ORM
{
    internal class CategoryDAO
    {
        private MyORM orm;
        public CategoryDAO(string connectionString)
        {
            orm = new MyORM(connectionString, "Categories");
        }

        public List<Category> GetAll()
            => orm.Select<Category>();

        public Category? Get(int id)
            => orm.Select<Category>(id);
    }
}
