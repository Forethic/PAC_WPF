﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T>
        where T : DomainObject
    {
        private readonly SimpleTraderDbContextFactory _ContextFactory;
        private readonly NonQueryDataService<T> _NonQueryDataService;

        public GenericDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _ContextFactory = contextFactory;
            _NonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await _NonQueryDataService.CreateAsync(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _NonQueryDataService.DeleteAsync(id);
        }

        public async Task<T> Get(int id)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _NonQueryDataService.UpdateAsync(id, entity);
        }
    }
}