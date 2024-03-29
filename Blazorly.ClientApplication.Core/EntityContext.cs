﻿using Blazorly.ClientApplication.Core.DBFactory;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using Blazorly.ClientApplication.SDK.Dto;
using Blazorly.ClientApplication.SDK.Interfaces;
using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Blazorly.ClientApplication.Core
{
    public class EntityContext : IEntityContext
    {
        private BaseDBFactory factory;
        public IAccessChecker AccessChecker { get; set; }

        public string CurrentUserID { get; set; }

        public EntityContext(BaseDBFactory dBFactory, IAccessChecker accessChecker = null) 
        {
            factory = dBFactory;
            this.AccessChecker = accessChecker;
        } 
        
        public async Task Create<T>(T entity) where T : BaseEntity
        {
            BaseEntity baseEntity = entity;
            baseEntity.Id = Nanoid.Generate();
            baseEntity.CreatedDate = DateTime.UtcNow;
            baseEntity.UpdatedDate = DateTime.UtcNow;
            baseEntity.CreatedBy = CurrentUserID;
            baseEntity.UpdatedBy = CurrentUserID;

            var item = ConvertExpando(entity);
            var collection = GetEntityName(typeof(T));
            if (!AccessChecker.HaveInsertAccess(collection, item))
                throw new UnauthorizedAccessException("Access denied to perform this operation");

            await factory.Insert(collection, item);
        }

        public async Task Delete<T>(string id) where T : BaseEntity
        {
            var collection = GetEntityName(typeof(T));
            if (!AccessChecker.HaveDeleteAccess(collection, id))
                throw new UnauthorizedAccessException("Access denied to perform this operation");

            await factory.Delete(collection, "Id", id);
        }

        public async Task<T> Get<T>(string id) where T : BaseEntity
        {
            var collection = GetEntityName(typeof(T));
            if (!AccessChecker.HaveReadAccess(collection, id))
                throw new UnauthorizedAccessException("Access denied to perform this operation");

            var data = await factory.Read(collection, "Id", id);
            return (T)data;
        }

        public async Task<ItemsResponse> Query<T>(ItemsQueryRequest query, int page = 1, int count = 100)
        {
            var collection = GetEntityName(typeof(T));
            query.MetaQuery = AccessChecker.GetMetaQuery(collection);
            var queryResult = await factory.Query(collection, query, page, count);

            return new ItemsResponse()
            {
                Count = queryResult.Count,
                HasNext = queryResult.HasNext,
                HasPrevious = queryResult.HasPrevious,
                PerPage = queryResult.PerPage,
                Success = true,
                TotalPages = queryResult.TotalPages,
                Data = queryResult.List.ToList()
            };
        }

        public async Task Update<T>(string id, T entity) where T : BaseEntity
        {
            BaseEntity baseEntity = entity;
            baseEntity.UpdatedDate = DateTime.UtcNow;
            baseEntity.UpdatedBy = CurrentUserID;

            var item = ConvertExpando(entity);
            var collection = GetEntityName(typeof(T));
            if (!AccessChecker.HaveUpdateAccess(collection, id))
                throw new UnauthorizedAccessException("Access denied to perform this operation");

            await factory.Update(collection, "Id", id, item);
        }

        private ExpandoObject ConvertExpando<T>(T entity)
        {
            var expando = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)expando;

            foreach (var property in entity.GetType().GetProperties())
            {
                var fieldDef = property.GetCustomAttribute<FieldDefAttribute>();
                if (fieldDef == null) continue;
                dictionary.Add(property.Name, property.GetValue(entity));
            }
            return expando;
        }

        private string GetEntityName(Type type)
        {
            var entityDef = type.GetCustomAttribute<EntityDefAttribute>();
            return entityDef?.Name;
        }
    }
}
