using FoodBuddy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodBuddy.Services.DataStores
{
    public abstract class BaseDataStore<Table> where Table : class, new()
    {
        protected FoodBuddyContext context = new FoodBuddyContext();
        protected DbSet<Table> table;

        public virtual async Task<IEnumerable<Table>> GetItemsAsync(bool forceRefresh = false, string sorter = null)
        {
            return await table.ToListAsync();
        }

        public async Task<Table> GetItemAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<bool> AddItemAsync(Table item)
        {
            try
            {
                await table.AddAsync(item);

                return await SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateItemAsync(Table item)
        {
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            Table item = await GetItemAsync(id);
            table.Remove(item);

            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            int ChangesSaved = await context.SaveChangesAsync();

            if (ChangesSaved != 0)
            {
                return true;
            }
            return false;
        }
    }
}
