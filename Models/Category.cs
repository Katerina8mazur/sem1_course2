using HttpServer_1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.Models
{
    internal class Category
    {
        public int Id { get; set; }

        [DBField("name")]
        public string Name { get; set; }

        [DBField("description")]
        public string Description { get; set; }

        [DBField("meal_time")]
        public string MealTime { get; set; }

        public Category(int id, string name, string description, string mealTime)
        {
            Id = id;
            Name = name;
            Description = description;
            MealTime = mealTime;
        }

        public Category()
        {
        }
    }
}
