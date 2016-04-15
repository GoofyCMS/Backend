using Goofy.Component.Authorization.Model;
using System;

namespace Goofy.Component.Authorization.Resources
{
    public class ClaimNameBuilder
    {
        public string GetClaimType(Type entityType, CrudOperation operation)
        {
            switch (operation)
            {
                case (CrudOperation.Create):
                    return "CanCreate" + entityType.Name;
                case (CrudOperation.Update):
                    return "CanUpdate" + entityType.Name;
                default:
                    return "CanDelete" + entityType.Name;
            }
        }
    }
}
