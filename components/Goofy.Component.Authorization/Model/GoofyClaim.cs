using System;

namespace Goofy.Component.Authorization.Model
{
    public class GoofyClaim : IEquatable<GoofyClaim>
    {
        public GoofyClaim(string claimType, string claimValue, bool isCrudClaim)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
            IsCrudClaim = isCrudClaim;
        }

        public string ClaimType { get; }

        public string ClaimValue { get; }

        public bool IsCrudClaim { get; }

        public bool Equals(GoofyClaim other)
        {
            if (IsCrudClaim != other.IsCrudClaim)
            {
                return false;
            }
            if (IsCrudClaim)
            {
                return string.Compare(ClaimType, other.ClaimType) == 0;
            }
            else
                return string.Compare(ClaimType, other.ClaimType) == 0 &&
                       string.Compare(ClaimValue, other.ClaimValue) == 0;
        }
    }
}
