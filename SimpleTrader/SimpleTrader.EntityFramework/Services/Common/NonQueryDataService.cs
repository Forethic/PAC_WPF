using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework.Services.Common
{
    public class NonQueryDataService<T>
        where T : DomainObject
    {
        private readonly SimpleTraderDbContextFactory _ContextFactory;

        public NonQueryDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _ContextFactory = contextFactory;
        }

        public async Task<T> CreateAsync(T entity)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}