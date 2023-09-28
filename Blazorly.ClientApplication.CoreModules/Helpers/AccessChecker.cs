using Blazorly.ClientApplication.CoreModules.Entities;
using Blazorly.ClientApplication.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Helpers
{
    public class AccessChecker
    {
        private Schema schema;

        public AccessChecker(Schema schema) 
        {
            this.schema = schema;
        }

        public bool HaveInsertAccess(string collection, List<SystemRolePermission> permissions)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Create == PermissionLevel.All) return true;

            if(permission.Create == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {
                
            }

            return false;
        }

        public bool HaveUpdateAccess(string collection, List<SystemRolePermission> permissions)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Update == PermissionLevel.All) return true;

            if (permission.Update == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {

            }

            return false;
        }

        public bool HaveDeleteAccess(string collection, List<SystemRolePermission> permissions)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Delete == PermissionLevel.All) return true;

            if (permission.Delete == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {

            }

            return false;
        }

        public bool HaveReadAccess(string collection, List<SystemRolePermission> permissions)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Update == PermissionLevel.All) return true;

            if (permission.Update == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {

            }

            return false;
        }
    }
}
