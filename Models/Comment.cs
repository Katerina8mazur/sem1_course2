using HttpServer_1.Attributes;
using HttpServer_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Models
{
    internal class Comment
    {
        
        public int Id { get; set; }

        [DBField("recipe_id")]
        public int RecipeId { get; set; }

        [DBField("author_id")]
        public int AuthorId { get; set; }

        [DBField("text")]
        public string Text { get; set; }

        public Account Author { get => Accounts.DAO.Get(AuthorId); }

        public Comment()
        {
        }

        public Comment(int id, int recipeId, int authorId, string text)
        {
            Id = id;
            RecipeId = recipeId;
            AuthorId = authorId;
            Text = text;
        }
    }
}
