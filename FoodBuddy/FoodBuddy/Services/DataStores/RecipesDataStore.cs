using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBuddy.Services.DataStores
{
    public class RecipesDataStore : BaseDataStore<Recipe>, IRecipesDataStore
    {
        public RecipesDataStore()
        {
            table = context.Recipes;
        }

        public async Task<IEnumerable<Recipe>> GetItemsByCategoryAsync(int categoryId, string sorter = null)
        {
            var recipes = table.Where(c => c.CategoryId == categoryId).Include(r => r.Category);
            return await Sort(sorter, recipes);
        }

        public override async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false, string sorter = null)
        {
            var recipes = table.Include(r => r.Category);
            return await Sort(sorter, recipes);
        }

        public async Task<IEnumerable<Recipe>> Sort(string sorter, IIncludableQueryable<Recipe, Category> recipes)
        {
            switch (sorter)
            {
                case "AZ":
                    var az = from recipe in recipes
                             orderby recipe.RecipeName ascending
                             select recipe;
                    return await az.ToListAsync();
                case "ZA":
                    var za = from recipe in recipes
                             orderby recipe.RecipeName descending
                             select recipe;
                    return await za.ToListAsync();
                case "Longest":
                    var longest = from recipe in recipes
                                  orderby recipe.RecipeDuration descending
                                  select recipe;
                    return await longest.ToListAsync();
                case "Shortest":
                    var shortest = from recipe in recipes
                                   orderby recipe.RecipeDuration ascending
                                   select recipe;
                    return await shortest.ToListAsync();
                case "Rating":
                    var rating = from recipe in recipes
                                 orderby recipe.RecipeRating descending
                                 select recipe;
                    return await rating.ToListAsync();
                case "Favorited":
                    var favorited = from recipe in recipes
                                     where recipe.RecipeFavorited == true
                                     select recipe;
                    return await favorited.ToListAsync();
                default:
                    return await recipes.ToListAsync();
            }
        }

        public async Task<IEnumerable<Recipe>> GetItemsByTagsAsync(string tag, string sorter = null)
        {
            var recipes = table.Where(r => r.Tags.Contains(tag)).Include(r => r.Category);
            return await Sort(sorter, recipes);
        }

        public async Task<IEnumerable<Recipe>> GetItemsByTagByCategoryAsync(string tag, int categoryId, string sorter = null)
        {
            var recipes = table.Where(r => r.CategoryId == categoryId).Where(r => r.Tags.Contains(tag)).Include(r => r.Category);
            return await Sort(sorter, recipes);
        }

        /*public Task<IEnumerable<Recipe>> GetItemsByTagByCategoryAsync(string tag, int categoryId, string sorter = null)
        {
            throw new NotImplementedException();
        }*/
    }
}
