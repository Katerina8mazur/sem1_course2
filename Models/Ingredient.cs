using HttpServer_1.Attributes;
using HttpServer_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Models
{
    internal class Ingredient
    {
        public int Id { get; set; }

        [DBField("ingredient_name")]
        public string Name { get; set; }

        [DBField("measure")]
        public string Measure { get; set; }

        public Ingredient(int id, string name, string measure)
        {
            Id = id;
            Name = name;
            Measure = measure;
        }

        public Ingredient()
        {
        }
    }
}
