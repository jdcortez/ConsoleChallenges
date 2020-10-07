using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeTwo.Repository
{
    public class ClaimsRepository : ClaimRepository
    {

        protected readonly Queue<Claim> _claimQueue = new Queue<Claim>();

        public bool EnqueueClaim(Claim claim)
        {
            AddClaim(claim);

            var count = _claimQueue.Count;
            if(claim!=null)
                _claimQueue.Enqueue(claim);
            
            bool wasAdded = (_claimQueue.Count > count) ? true : false;
            return wasAdded;
                
        }

        public Claim GetNextClaim()
        {
            return _claimQueue.Peek();
        }

        public bool DeleteNextClaim()
        {
            if (_claimQueue.Count < 1)
                return false;

            var claim =_claimQueue.Dequeue();
            RemoveClaim(claim);
            return true;
        }

        public int GetCount()
        {
            return _claimQueue.Count;
        }







    
    }
}
