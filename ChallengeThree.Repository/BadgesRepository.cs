using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeThree.Repository
{
    public class BadgesRepository : BadgeRepository
    {
        public void UpdateDoorsOnExistingBadge(Badge badge, List<string> doors)
        {
            if (KeyExists(badge))
            {
                //instantiates new badge with same id and new doors
                var newBadge = new Badge(badge.Id, doors);
                //sets new value for existing key
                _badgeDirectory[badge] = newBadge;
            }
        }

        public void DeleteAllDoorsOnExistingBadge(Badge badge)
        {
            if (KeyExists(badge))
            {
                //instantiates new badge with same id and empty door list
                var newBadge = new Badge();
                newBadge.Id = badge.Id;
                //sets new value for existing key
                _badgeDirectory[badge] = newBadge;
            }
        }
    }
}
