using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string? collection) : base($"Access denied to collection: {collection}")
        {
        }
    }
}
