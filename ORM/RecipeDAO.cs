using HttpServer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.ORM
{
    internal class RecipeDAO
    {
        private MyORM orm;
        public RecipeDAO(string connectionString)
        {
            orm = new MyORM(connectionString, "Recipes");
        }

        public List<Recipe> GetAll()
            => orm.Select<Recipe>();

        public Recipe? Get(int id)
            => orm.Select<Recipe>(id);

        public int Insert(string name, int categoryId, int authorId, string text, string ingredients)
            => orm.Insert(new Recipe()
            { Name = name, CategoryId = categoryId, AuthorId = authorId, Text = text, IngredientsString = ingredients });

        public void Edit(int recipeId, string name, int categoryId, string text, string ingredients)
        {
            var recipe = orm.Select<Recipe>(recipeId);
            recipe.Name = name;
            recipe.CategoryId = categoryId;
            recipe.IngredientsString = ingredients;
            recipe.Text = text;
            orm.Update(recipeId, recipe);
        }

        public void Delete(int recipeId) 
            => orm.Delete(recipeId);
    }
}
