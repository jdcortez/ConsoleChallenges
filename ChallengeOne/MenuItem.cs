using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOne.Repository
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }

        public MenuItem()
        {
            Ingredients = new List<string>();
        }

        public MenuItem(int id, string name, string description, List<string> ingredients, double price)
        {
            Id = id;
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Price = price;
        }


    }
}
