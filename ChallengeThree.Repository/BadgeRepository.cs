using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeThree.Repository
{
    public class BadgeRepository
    {
        protected readonly Dictionary<Badge, Badge> _badgeDirectory = new Dictionary<Badge, Badge>();

        public bool AddBadge(Badge badge)
        {
            int startingCount = _badgeDirectory.Count;

            //if badge is not null and badge isn't already a key in dictionary
            if (badge != null && !KeyExists(badge))
            {
                _badgeDirectory.Add(badge, badge);
            }

            bool wasAdded = (startingCount < _badgeDirectory.Count) ? true : false;
            return wasAdded;
        }
        public Dictionary<Badge, Badge> GetAllBadges()
        {
            return _badgeDirectory;
        }

        public bool KeyExists(Badge badge)
        {
            return _badgeDirectory.ContainsKey(badge);
        }

        public Badge ReturnValueWithKey(Badge badge)
        {
            if (KeyExists(badge))
            {
                return _badgeDirectory[badge];
            }
            else
            {
                return null;
            }
        }

        public void UpdateBadge(Badge badge)
        {
            if (KeyExists(badge))
            {
                _badgeDirectory[badge] = badge;
            }
        }

        public bool DeleteBadge(Badge badge)
        {
            return _badgeDirectory.Remove(badge);
        }

    }
}
