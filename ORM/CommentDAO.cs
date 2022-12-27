using HttpServer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.ORM
{
    internal class CommentDAO
    {
        private MyORM orm;
        public CommentDAO(string connectionString)
        {
            orm = new MyORM(connectionString, "Comments");
        }

        public List<Comment> GetAll()
            => orm.Select<Comment>();

        public Comment? Get(int id)
            => orm.Select<Comment>(id);

        public List<Comment> GetByPublicationId(int id)
            => GetAll().Where(c => c.RecipeId == id).ToList();

        internal void Insert(int recipeId, string text, int authorId)
            => orm.Insert(new Comment() { RecipeId = recipeId, AuthorId = authorId, Text = text });
    }
}
