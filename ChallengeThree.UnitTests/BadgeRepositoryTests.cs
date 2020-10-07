using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeThree.Repository;
using System.Collections.Generic;

namespace ChallengeThree.UnitTests
{
    [TestClass]
    public class BadgeRepositoryTests
    {
        [TestMethod]
        public void AddBadge_BadgeIsAdded_ReturnsTrue()
        {
            var badge = new Badge();
            var badgeRepository = new BadgeRepository();

            bool wasAdded = badgeRepository.AddBadge(badge);

            Assert.IsTrue(wasAdded);
        }
       
        [TestMethod]
        public void AddBadge_BadgeNotAddedBecauseNull_ReturnsFalse()
        {
            Badge nullBadge = null;
            var badgeRepository = new BadgeRepository();

            bool wasAdded = badgeRepository.AddBadge(nullBadge);

            Assert.IsFalse(wasAdded);
        }
        
        [TestMethod]
        public void AddBadge_BadgeNotAddedBecauseAlreadyExists_ReturnsFalse()
        {
            Badge newBadge = new Badge();
            var badgeRepository = new BadgeRepository();
            badgeRepository.AddBadge(newBadge);

            bool wasAdded = badgeRepository.AddBadge(newBadge);

            Assert.IsFalse(wasAdded);
        }

        [TestMethod]
        public void GetAllBadges_BadgesReturned_DictionaryHasCountTwo()
        {
            var firstBadge = new Badge(1, new List<string>());
            var secondBadge = new Badge(2, new List<string>());
            var badgeRepository = new BadgeRepository();
            badgeRepository.AddBadge(firstBadge);
            badgeRepository.AddBadge(secondBadge);

            var itemsReturned = badgeRepository.GetAllBadges().Count;

            Assert.AreEqual(2, itemsReturned);
        }

        [TestMethod]
        public void KeyExists_KeyIsFound_ReturnsTrue()
        {
            var firstBadge = new Badge(1, new List<string>());
            var badgeRepository = new BadgeRepository();
            badgeRepository.AddBadge(firstBadge);

            var keyFound = badgeRepository.KeyExists(firstBadge);

            Assert.IsTrue(keyFound);
        }

        [TestMethod]
        public void KeyExists_KeyNotFound_ReturnsFalse()
        {
            var badgeRepository = new BadgeRepository();

            var keyFound = badgeRepository.KeyExists(new Badge());

            Assert.IsFalse(keyFound);
        }

        [TestMethod]
        public void DeleteBadge_BadgeDeleted_ReturnsTrue()
        {
            var badge = new Badge();
            var badgeRepository = new BadgeRepository();
            badgeRepository.AddBadge(badge);

            var wasDeleted = badgeRepository.DeleteBadge(badge);

            Assert.IsTrue(wasDeleted);
        }

        [TestMethod]
        public void DeleteBadge_BadgeNotDeleted_ReturnsFalse()
        {
            var badgeRepository = new BadgeRepository();

            var wasDeleted = badgeRepository.DeleteBadge(new Badge());

            Assert.IsFalse(wasDeleted);
        }

        [TestMethod]
        public void ReturnsValueWithKey_ReturnsValuefromExistingKey_ReturnsValue()
        {
            var badge = new Badge();
            var badgeRepository = new BadgeRepository();
            badgeRepository.AddBadge(badge);

            var returnedBadge = badgeRepository.ReturnValueWithKey(badge);

            Assert.IsNotNull(returnedBadge);
        }

        [TestMethod]
        public void ReturnsValueWithKey_KeyNotFound_ReturnsNull()
        {
            var badge = new Badge(1, new List<string>());
            var badgeRepository = new BadgeRepository();
            badgeRepository.AddBadge(badge);

            var returnedBadge = badgeRepository.ReturnValueWithKey(new Badge());

            Assert.IsNull(returnedBadge);
        }



    }
}
