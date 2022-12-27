using HttpServer_1.Attributes;
using HttpServer_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HttpServer_1.Models
{
    internal class Recipe
    {
        public class Paragraph
        {
            public Paragraph(string image, string text)
            {
                Image = image;
                Text = text;
            }

            public string Image { get; set; }
            public string Text { get; set; }
        }

        public int Id { get; set; }

        [DBField("name")]
        public string Name { get; set; }

        [DBField("category_id")]
        public int CategoryId { get; set; }

        [DBField("author_id")]
        public int AuthorId { get; set; }

        [DBField("text")]
        public string Text { get; set; }

        [DBField("ingredients")]
        public string IngredientsString { get; set; }

        public List<string> Ingredients { get => IngredientsString.Split("\\n").ToList(); }
        public Account Author { get => Accounts.DAO.Get(AuthorId); }
        public Category Category { get => Categories.DAO.Get(CategoryId); }
        public List<Comment> Comments { get => Controllers.Comments.DAO.GetByPublicationId(Id); }
        public List<Paragraph> Paragraphs { get => Text
                .Split("\\n")
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(x => x.Index / 2)
                .Select(g => new Paragraph(g.ElementAt(0).Value, g.ElementAt(1).Value))
                .ToList();
        }
        public string Image { get => Paragraphs.Last().Image; }


        public Recipe(int id, string name, int categoryId, int authorId, string text, string ingredients)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            AuthorId = authorId;
            Text = text;
            IngredientsString = ingredients;
        }

        public Recipe()
        {
        }
    }
}
