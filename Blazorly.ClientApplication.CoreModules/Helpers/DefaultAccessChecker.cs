using Blazorly.ClientApplication.CoreModules.Entities;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Dto;
using Blazorly.ClientApplication.SDK.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Helpers
{
    public class DefaultAccessChecker : IAccessChecker
    {
        List<SystemRolePermission> permissions;

        IDBFactoryExtension factoryExtension;

        public DefaultAccessChecker(IDBFactoryExtension factoryExtension, List<SystemRolePermission> permissions) 
        {
            this.factoryExtension = factoryExtension;
            this.permissions = permissions;
        }

        public bool HaveInsertAccess(string collection, ExpandoObject data)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Create == PermissionLevel.All) return true;

            if(permission.Create == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {
                
            }

            return false;
        }

        public bool HaveUpdateAccess(string collection, string id)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Update == PermissionLevel.All) return true;

            if (permission.Update == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {
                return CheckAccess(collection, id, permission);
            }

            return false;
        }

        public bool HaveDeleteAccess(string collection, string id)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Delete == PermissionLevel.All) return true;

            if (permission.Delete == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {
                return CheckAccess(collection, id, permission);
            }

            return false;
        }

        public bool HaveReadAccess(string collection, string id)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return false;

            if (permission.Read == PermissionLevel.All) return true;

            if (permission.Read == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {
                return CheckAccess(collection, id, permission);
            }

            return false;
        }

        private bool CheckAccess(string collection, string id, SystemRolePermission? permission)
        {
            Dictionary<string, object> filters = new Dictionary<string, object>();
            filters.Add("Id", id);

            Dictionary<string, object> metaQueryDict = JsonSerializer.Deserialize<Dictionary<string, object>>(permission.RestrictMeta);
            foreach (var item in metaQueryDict)
            {
                filters.Add(item.Key, item.Value);
            }

            return factoryExtension.CheckRecordAccess(collection, filters).Result;
        }

        public Dictionary<string, object> GetMetaQuery(string collection)
        {
            var permission = permissions.Where(x => x.Entity.ToUpper() == collection.ToUpper()).FirstOrDefault();
            if (permission == null) return new Dictionary<string, object>();

            if (permission.Read == PermissionLevel.All) return new Dictionary<string, object>();

            if (permission.Read == PermissionLevel.Restricted && !string.IsNullOrWhiteSpace(permission.RestrictMeta))
            {
                Dictionary<string, object> metaQueryDict = JsonSerializer.Deserialize<Dictionary<string, object>>(permission.RestrictMeta);
                return metaQueryDict;
            }

            return new Dictionary<string, object>();
        }
    }
}
