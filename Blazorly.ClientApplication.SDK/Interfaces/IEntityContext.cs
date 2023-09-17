using Blazorly.ClientApplication.SDK.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Interfaces
{
    public interface IEntityContext
    {
        Task Create<T>(T entity) where T : BaseEntity;

        Task Update<T>(string id, T entity) where T : BaseEntity;

        Task Delete<T>(string id) where T : BaseEntity;

        Task<T> Get<T>(string id) where T : BaseEntity;

        Task<ItemsResponse> Query<T>(ItemsQueryRequest query, int page = 1, int count = 100);
    }
}
