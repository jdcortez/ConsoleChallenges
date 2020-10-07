using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeThree.Repository
{
    public class Badge
    {
        public List<String> Doors { get; set; }
        public int Id { get; set; }

        public Badge()
        {
            Doors = new List<String>(); 
        }

        public Badge(int id, List<String> doors)
        {
            Id = id;
            Doors = doors;
        }

        //override GetHashCode() and Equals() to compare object references based on id field
        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Badge);
        }
        public bool Equals(Badge obj)
        {
            return obj != null && obj.Id == this.Id;
        }

    }
}
