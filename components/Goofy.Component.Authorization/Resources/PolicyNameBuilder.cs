using Goofy.Component.Authorization.Model;
using System;

namespace Goofy.Component.Authorization.Resources
{
    public class PolicyNameBuilder
    {
        public string GetPolicyName(Type entityType, CrudOperation operation)
        {
            switch (operation)
            {
                case (CrudOperation.Create):
                    return "RequireCreate" + entityType.Name;
                case (CrudOperation.Update):
                    return "RequireUpdate" + entityType.Name;
                case (CrudOperation.View):
                    return "RequireView" + entityType.Name;
                default:
                    return "RequireDelete" + entityType.Name;
            }
        }
    }
}
