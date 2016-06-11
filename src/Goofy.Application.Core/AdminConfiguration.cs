using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goofy.Application.Core
{
    public class AdminConfiguration
    {
        public string AdmininstrationRoleName { get; set; } = "Administrator";
        public string AdministrationRoleDescription { get; set; } = "Role that represent an administrator user";
        public string AdminUsername { get; set; } = "admin@goofy.com";
        public string AdminPassword { get; set; } = "Admin!234";
        public string AdminEmail { get; set; } = "admin@goofy.com";
    }
}
