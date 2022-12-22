using HttpServer_1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Models
{
    internal class Recipe
    {
        public int Id { get; set; }

        [DBField("name")]
        public string Name { get; set; }

        [DBField("category_id")]
        public int CategoryId { get; set; }

        [DBField("account_id")]
        public int AccountId { get; set; }

        [DBField("text")]
        public string Text { get; set; }

        [DBField("picture")]
        public string Picture { get; set; }

        public Recipe(int id, string name, int categoryId, int accountId, string text)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            AccountId = accountId;
            Text = text;
        }

        public Recipe()
        {
        }
    }
}
