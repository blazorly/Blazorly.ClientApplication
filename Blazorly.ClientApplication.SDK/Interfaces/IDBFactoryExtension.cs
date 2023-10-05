using Blazorly.ClientApplication.SDK.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Interfaces
{
    public interface IDBFactoryExtension
    {
        Schema GetSchema();

        Task<bool> CheckRecordAccess(string collection, Dictionary<string, object> filters);
    }
}
