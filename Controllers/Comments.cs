using HttpServer_1.Attributes;
using HttpServer_1.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Controllers
{
    [HttpController("comments")]
    internal class Comments: Controller
    {
        private static CommentDAO commentDAO = new CommentDAO(connectionString);
        public static CommentDAO DAO { get => commentDAO; }

        [HttpPOST("^$")]
        [OnlyForAuthorized]
        [NeedAccountId]
        public MethodResponse CreateComment(int recipeId, string text, int authorId)
        {
            if (Recipes.DAO.Get(recipeId) == null || Accounts.DAO.Get(authorId) == null)
                throw new ServerException(System.Net.HttpStatusCode.NotFound);
            commentDAO.Insert(recipeId, text, authorId);
            return new MethodResponse($"/recipes/{recipeId}");
        }
    }
}
