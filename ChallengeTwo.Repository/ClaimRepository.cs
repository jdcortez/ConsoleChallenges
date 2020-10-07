using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeTwo.Repository
{
    public class ClaimRepository
    {
        protected readonly List<Claim> _claimDirectory = new List<Claim>();

        //returns true if claimItem was added to list, otherwise false
        public bool AddClaim(Claim claim)
        {
            int startingCount = _claimDirectory.Count;

            if (claim != null)
                _claimDirectory.Add(claim);

            bool wasAdded = (startingCount < _claimDirectory.Count) ? true : false;
            return wasAdded;
        }
        public List<Claim> GetAllClaims()
        {
            return _claimDirectory;
        }

        public bool RemoveClaim(Claim claim)
        {
            bool resultDeleted = _claimDirectory.Remove(claim);
            return resultDeleted;
        }

    }
}
