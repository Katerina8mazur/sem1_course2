using HttpServer_1.Attributes;
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

        [DBField("account_id")]
        public int AccountId { get; set; }

        [DBField("comment_text")]
        public string CommentText { get; set; }

        public Comment()
        {
        }

        public Comment(int id, int recipeId, int accountId, string commentText)
        {
            Id = id;
            RecipeId = recipeId;
            AccountId = accountId;
            CommentText = commentText;
        }
    }
}
