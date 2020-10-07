using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeTwo.Repository;

namespace ChallengeTwo.UnitTests
{
    [TestClass]
    public class ClaimsRepositoryTests
    {
        [TestMethod]
        public void EnqueueClaim_AddsClaimToQueue_ReturnsTrue()
        {
            var claim = new Claim();
            var claimsRepository = new ClaimsRepository();

            bool wasAdded = claimsRepository.EnqueueClaim(claim);

            Assert.IsTrue(wasAdded);
        }

        [TestMethod]
        public void EnqueueClaim_ClaimIsNull_ReturnsFalse()
        {
            Claim claim = null;
            var claimsRepository = new ClaimsRepository();

            bool wasAdded = claimsRepository.EnqueueClaim(claim);

            Assert.IsFalse(wasAdded);
        }

        [TestMethod]
        public void GetNextClaim_GetsClaimAtHeadOfQueue_ReturnsExpectedClaim()
        {
            var firstIn = new Claim();
            var secondIn = new Claim();
            var claimsRepository = new ClaimsRepository();

            claimsRepository.EnqueueClaim(firstIn);
            claimsRepository.EnqueueClaim(secondIn);
            var head = claimsRepository.GetNextClaim();

            Assert.AreEqual(head, firstIn);
        }

        [TestMethod]
        public void DeleteNextClaim_DeletesClaimFromQueue_ExpectedEmptyQueueAfterEnqueueDequeue()
        {
            var claim = new Claim();
            var claimsRepository = new ClaimsRepository();

            claimsRepository.EnqueueClaim(claim);
            claimsRepository.DeleteNextClaim();
            int count = claimsRepository.GetCount();

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void DeleteNextClaim_QueueIsEmpty_ReturnsFalse()
        {
            var claimsRepository = new ClaimsRepository();

            var claimDeleted = claimsRepository.DeleteNextClaim();

            Assert.IsFalse(claimDeleted);
        }

        [TestMethod]
        public void GetCount_GetsNumberOfClaimsFromQueueAfterTwoEnqueus_ReturnsTwo()
        {
            var firstIn = new Claim();
            var secondIn = new Claim();
            var claimsRepository = new ClaimsRepository();

            claimsRepository.EnqueueClaim(firstIn);
            claimsRepository.EnqueueClaim(secondIn);
            var count = claimsRepository.GetCount();

            Assert.AreEqual(2, count);
        }
    }
}
