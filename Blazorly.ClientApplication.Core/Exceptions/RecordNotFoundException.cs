using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string collection) : base($"Record not found or don't have access in collection {collection}")
        {
        }
    }
}
