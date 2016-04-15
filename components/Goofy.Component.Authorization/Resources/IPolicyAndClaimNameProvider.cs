using System;

namespace Goofy.Component.Authorization.Resources
{
    public interface IPolicyAndClaimNameProvider
    {
        string GetCreateClaim(Type entityType);

        string GetUpdateClaim(Type entityType);

        string GetDeleteClaim(Type entityType);

        string GetCreatePolicy(Type entityType);

        string GetUpdatePolicy(Type entityType);

        string GetDeletePolicy(Type entityType);
    }
}
