using FoodBuddy.Models;
using FoodBuddy.Services.DataStores;
using FoodBuddy.ViewModels.Recipes;
using FoodBuddy.Services.Interfaces;
using FoodBuddy.Views.Recipes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FoodBuddy.ViewModels.Recipes
{
    public class RecipesViewModel : BaseViewModel
    {
        public IRecipesDataStore DataStore => DependencyService.Get<IRecipesDataStore>();
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Category category;

        public RecipesViewModel(Category category = null)
        {
            Title = "Recipes";
            this.category = category;
            Recipes = new ObservableCollection<Recipe>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(category));
            SortCommand = new Command(async (parameter) => await ExecuteLoadItemsCommand(category, (string)parameter));

            MessagingCenter.Subscribe<NewRecipeViewModel, Recipe>(this, "AddRecipe", async (obj, recipe) =>
            {
                Recipes.Add(recipe);
                await DataStore.AddItemAsync(recipe);
                await ExecuteLoadItemsCommand(category);
            });

            MessagingCenter.Subscribe<RecipeDetailViewModel, Recipe>(this, "DeleteRecipe", async (obj, recipe) =>
            {
                await DeleteRecipe(recipe);
            });
        }

        public async Task DeleteRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
            await DataStore.DeleteItemAsync(recipe.RecipeId);
            await ExecuteLoadItemsCommand(category);
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            await DataStore.UpdateItemAsync(recipe);
        }

        async Task ExecuteLoadItemsCommand(Category category = null, string sorter = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Recipes.Clear();
                IEnumerable<Recipe> recipes;
                if (category != null)
                {
                    recipes = await DataStore.GetItemsByCategoryAsync(category.CategoryId, sorter);
                }
                else
                {
                    recipes = await DataStore.GetItemsAsync(true, sorter);
                }
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ExecutePerformSearchCommand(string search, Category category = null, string sorter = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Recipes.Clear();
                IEnumerable<Recipe> recipes;
                if (category != null)
                {
                    recipes = await DataStore.GetItemsByTagByCategoryAsync(search, category.CategoryId, sorter);
                }
                else
                {
                    recipes = await DataStore.GetItemsByTagsAsync(search, sorter);
                }
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
