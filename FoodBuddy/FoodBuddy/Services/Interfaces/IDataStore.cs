using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodBuddy.Services.Interfaces
{
    public interface IDataStore<Table>
    {
        Task<Table> GetItemAsync(int id);
        Task<IEnumerable<Table>> GetItemsAsync(bool forceRefresh = false, string sorter = null);
        Task<IEnumerable<Table>> GetItemsByTagsAsync(string tag, string sorter = null);
        Task<bool> AddItemAsync(Table item);
        Task<bool> UpdateItemAsync(Table item);
        Task<bool> DeleteItemAsync(int id);
    }
}
