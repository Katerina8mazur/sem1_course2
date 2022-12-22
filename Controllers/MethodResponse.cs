using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Controllers
{
    internal class MethodResponse
    {
        public View View { get; set; }
        public string Redirect { get; set; }

        public Cookie? Cookie { get; set; }

        public MethodResponse(string redirect, Cookie? cookie = null)
        {
            Redirect = redirect;
            Cookie = cookie;
        }

        public MethodResponse(View view, Cookie? cookie = null)
        {
            View = view;
            Cookie = cookie;
        }
    }
}