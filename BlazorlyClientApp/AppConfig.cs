﻿using Blazorly.ClientApplication.Core;
using Blazorly.ClientApplication.Core.DB;
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

        public static DBFactory Factory
        {
            get
            {
                return new DBFactory(DBType, DBConnectionString, DBTimeout); 
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
    }
}
