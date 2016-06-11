
namespace Goofy.Security
{
    public static class SecurityUtils
    {
        public static string GetPermissionName(string resource, CrudOperation crudOperation)
        {
            var operation = GetOperationName(crudOperation);
            return $"Can{operation}{resource}";
        }

        public static string GetPermissionDescription(string resource, CrudOperation crudOperation)
        {
            var operation = GetOperationName(crudOperation);
            return $"Permission for performing \"{operation}\" operations over resource \"{resource}\" ";
        }

        public static string GetPolicyName(string resource, CrudOperation crudOperation)
        {
            var operation = GetOperationName(crudOperation);
            return $"Require{operation}{resource}";
        }

        static string GetOperationName(CrudOperation crudOperation)
        {
            string permission;
            switch (crudOperation)
            {
                case CrudOperation.Create:
                    {
                        permission = "Create";
                        break;
                    }
                case CrudOperation.Read:
                    {
                        permission = "Read";
                        break;
                    }
                case CrudOperation.Update:
                    {
                        permission = "Update";
                        break;
                    }
                default:
                    {
                        permission = "Delete";
                        break;
                    }
            }

            return permission;
        }
    }
}