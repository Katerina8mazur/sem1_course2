using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;
using XSystem.Security.Cryptography;

namespace HttpServer_1
{
    internal class Hash
    {
        public static string Calculate(string data)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Encoding.UTF8.GetString(md5data);
        }
    }
}
