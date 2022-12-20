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
    }
}
