﻿using Blazorly.ClientApplication.Core;
using Blazorly.ClientApplication.Core.DBFactory;
using Blazorly.ClientApplication.CoreModules.Entities;
using Blazorly.ClientApplication.CoreModules.Helpers;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Interfaces;
using System.Net.NetworkInformation;

namespace BlazorlyClientApp
{
    public class AppConfig
    {
        public static string DBConnectionString { get; set; }

        public static string DBType { get; set; }

        public static int DBTimeout { get; set; }

        public static List<string> Modules { get; set; }

        public static PageBuilder PageBuilder { get; set; } = new PageBuilder();

        public static BaseDBFactory Factory
        {
            get
            {
                switch (DBType)
                {
                    case "MSSQL":
                        return new MSSQLDBFactory(DBConnectionString, DBTimeout);
                    default:
                        break;
                }

                throw new NotImplementedException();
            }
        }

        public static Schema Schema { get; set; }

        public static void LoadModules()
        {
            foreach (var item in Modules)
            {
                PageBuilder.LoadModuleLibrary(item);
            }
        }

        public static EntityContext GetEntityContext(string currentUserId)
        {
            var context = new EntityContext(Factory);
            context.CurrentUserID = currentUserId;
            return context;
        }

        public static IAccessChecker GetAccessChecker(string currentUserId)
        {
            var context = GetEntityContext(currentUserId);
            //context.Query<SystemRolePermission>(new Blazorly.ClientApplication.SDK.Dto.ItemsQueryRequest()
            //{
            //    Filter = new 
            //    {

            //    }
            //});

            return new DefaultAccessChecker(Factory, null);
        }
    }
}
