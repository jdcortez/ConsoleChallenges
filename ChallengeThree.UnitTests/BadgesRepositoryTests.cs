using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeThree.Repository;
using System.Collections.Generic;

namespace ChallengeThree.UnitTests
{
    [TestClass]
    public class BadgesRepositoryTests
    {
        [TestMethod]
        public void UpdateDoorsOnExistingBadge_BadgeIsUpdatedWithNewList_ListHasNewValues()
        {
            //badge with id 1 and empty list is added to repository
            var badge = new Badge(1, new List<string>());
            var badgesRepository = new BadgesRepository();
            badgesRepository.AddBadge(badge);
            
            //badged with id 1 gets updated list
            List<string> newList = new List<string>() { "a3" };
            badgesRepository.UpdateDoorsOnExistingBadge(badge, newList);
            var updatedBadge = badgesRepository.ReturnValueWithKey(badge);
            
            //list is copied from updated badge
            List<string> list = updatedBadge.Doors;

            //assert list contains string "a3"
            StringAssert.Equals("a3", list[0]);
        }

        public void DeleteAllDoorsOnExistingBadge_BadgeIsUpdatedWithEmptyList_ListIsEmpty()
        {
            //badge with id 1 and list is added to repository
            var badge = new Badge(1, new List<string>() { "a1", "a2", "a3"});
            var badgesRepository = new BadgesRepository();
            badgesRepository.AddBadge(badge);

            //badge with id 1 gets list cleared
            badgesRepository.DeleteAllDoorsOnExistingBadge(badge);
            var updatedBadge = badgesRepository.ReturnValueWithKey(badge);

            //list is copied from updated badge
            List<string> list = updatedBadge.Doors;

            //assert list is now empty
            Assert.Equals(0, list.Count);
        }




    }
}
