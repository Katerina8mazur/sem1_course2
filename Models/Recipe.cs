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

        [DBField("dish_name")]
        public string Name { get; set; }

        [DBField("category_id")]
        public int CategoryId { get; set; }

        [DBField("account_id")]
        public int AccountId { get; set; }

        [DBField("recipe_text")]
        public string RecipeText { get; set; }

        public Recipe(int id, string name, int categoryId, int accountId, string recipeText)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            AccountId = accountId;
            RecipeText = recipeText;
        }

        public Recipe()
        {
        }
    }
}
