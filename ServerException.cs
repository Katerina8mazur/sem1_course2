using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1
{
    internal class ServerException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ServerException(HttpStatusCode statusCode) : base()
        {
            StatusCode = statusCode;
        }   
    }
}
