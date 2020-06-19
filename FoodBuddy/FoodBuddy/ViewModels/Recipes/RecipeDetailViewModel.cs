using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Recipes
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        public IRecipesDataStore DataStore => DependencyService.Get<IRecipesDataStore>();
        public Recipe Recipe { get; set; }
        public RecipeDetailViewModel(Recipe recipe = null)
        {
            Title = recipe?.RecipeName;
            Recipe = recipe;
            DeserializeTags();

            MessagingCenter.Subscribe<EditRecipeViewModel, Recipe>(this, "EditRecipe", async (obj, update) =>
            {
                Recipe = update;
                Title = update.RecipeName;
                DeserializeTags();
                await DataStore.UpdateItemAsync(update);
            });
        }

        public void Delete()
        {
            MessagingCenter.Send(this, "DeleteRecipe", Recipe);
        }

        public void DeserializeTags()
        {
            Tags.Clear();
            string[] tags = Recipe.Tags.Split(',');
            foreach (string tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}
