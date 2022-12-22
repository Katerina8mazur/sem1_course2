using HttpServer_1.Controllers;
using Scriban;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1
{
    internal class View
    {
        private string htmlFilePath;
        private object model;

        public View(string htmlFilePath, object model = null)
        {
            this.htmlFilePath = htmlFilePath;
            this.model = (model != null) ? model : new { };
        }

        public byte[] Render(string sitePath)
        {
            var file = FileLoader.GetFile(sitePath, htmlFilePath, out string _);
            var html = Encoding.UTF8.GetString(file);
            var template = Template.Parse(html);
            var result = template.Render(model);
            return Encoding.UTF8.GetBytes(result);
        }
    }
}
