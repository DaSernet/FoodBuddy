using FoodBuddy.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodBuddy.Services.Interfaces
{
    public interface IRecipesDataStore : IDataStore<Recipe>
    {
        Task<IEnumerable<Recipe>> GetItemsByCategoryAsync(int categoryId, string sorter = null);
        Task<IEnumerable<Recipe>> Sort(string sorter, IIncludableQueryable<Recipe, Category> categories);
        Task<IEnumerable<Recipe>> GetItemsByTagByCategoryAsync(string tag, int categoryId, string sorter = null);
    }
}
