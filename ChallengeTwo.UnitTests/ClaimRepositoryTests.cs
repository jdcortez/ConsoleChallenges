using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeTwo.Repository;

namespace ChallengeTwo.UnitTests
{
    [TestClass]
    public class ClaimRepositoryTests
    {
        [TestMethod]
        public void AddClaim_ClaimAdded_ReturnsTrue()
        {
            var claim = new Claim();
            var claimRepository = new ClaimRepository();

            bool wasAdded = claimRepository.AddClaim(claim);

            Assert.IsTrue(wasAdded);
        }

        [TestMethod]
        public void AddClaim_ClaimIsNull_ReturnsFalse()
        {
            Claim claim = null;
            var claimRepository = new ClaimRepository();

            bool wasAdded = claimRepository.AddClaim(claim);

            Assert.IsFalse(wasAdded);
        }

        [TestMethod]
        public void GetAllClaims_ClaimsReturned_ReturnsList()
        {
            var firstClaim = new Claim();
            var secondClaim = new Claim();
            var claimRepository = new ClaimRepository();
            claimRepository.AddClaim(firstClaim);
            claimRepository.AddClaim(secondClaim);

            var itemsReturned = claimRepository.GetAllClaims().Count;

            Assert.AreEqual(2, itemsReturned);
        }

        [TestMethod]
        public void RemoveClaim_ClaimRemoved_ReturnsTrue()
        {
            var firstClaim = new Claim();
            var claimRepository = new ClaimRepository();
            claimRepository.AddClaim(firstClaim);

            var wasDeleted = claimRepository.RemoveClaim(firstClaim);

            Assert.IsTrue(wasDeleted);
        }

        [TestMethod]
        public void RemoveClaim_ClaimNotRemoved_ReturnsFalse()
        {
            var firstClaim = new Claim();
            var claimRepository = new ClaimRepository();
            claimRepository.AddClaim(firstClaim);

            var wasDeleted = claimRepository.RemoveClaim(new Claim());

            Assert.IsFalse(wasDeleted);
        }

    }//end class
}
