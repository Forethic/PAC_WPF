using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly SimpleTraderDbContextFactory _ContextFactory;
        private readonly NonQueryDataService<Account> _NonQueryDataService;

        public AccountDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _ContextFactory = contextFactory;
            _NonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Create(Account entity)
        {
            return await _NonQueryDataService.CreateAsync(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _NonQueryDataService.DeleteAsync(id);
        }

        public async Task<Account> Get(int id)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                Account entity = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _NonQueryDataService.UpdateAsync(id, entity);
        }

        public Task<Account> GetByEmail(string email)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                return context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);
            }
        }

        public async Task<Account> GetByUsername(string username)
        {
            using (SimpleTraderDbContext context = _ContextFactory.CreateDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(a => a.AccountHolder.Username == username);
            }
        }
    }
}