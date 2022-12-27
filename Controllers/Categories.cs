using HttpServer_1.Attributes;
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
    }
}
