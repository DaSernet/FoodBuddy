using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBuddy.Services.DataStores
{
    public class CategoriesDataStore : BaseDataStore<Category>, ICategoriesDataStore
    {
        public CategoriesDataStore()
        {
            table = context.Categories;
        }

        public override async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false, string sorter = null)
        {
            var categories = table;
            return await Sort(sorter, categories);
        }

        public async Task<IEnumerable<Category>> GetItemsByTagsAsync(string tag, string sorter = null)
        {
            var categories = table.Where(c => c.Tags.Contains(tag));
            return await Sort(sorter, categories);
        }

        public async Task<IEnumerable<Category>> Sort(string sorter, IQueryable<Category> categories)
        {
            switch (sorter)
            {
                case "AZ":
                    var az = from category in categories
                             orderby category.CategoryName ascending
                             select category;
                    return await az.ToListAsync();
                case "ZA":
                    var za = from category in categories
                             orderby category.CategoryName descending
                             select category;
                    return await za.ToListAsync();
                default:
                    return await categories.ToListAsync();
            }
        }
    }
}
