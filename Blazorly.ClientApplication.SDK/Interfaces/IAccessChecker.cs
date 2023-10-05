using Blazorly.ClientApplication.SDK.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Interfaces
{
    public interface IAccessChecker
    {
        bool HaveInsertAccess(string collection, ExpandoObject data);
        bool HaveUpdateAccess(string collection, string id);
        bool HaveDeleteAccess(string collection, string id);
        bool HaveReadAccess(string collection, string id);
        Dictionary<string, object> GetMetaQuery(string collection);
    }
}
