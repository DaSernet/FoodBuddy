using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Recipes
{
    public class EditRecipeViewModel : BaseViewModel
    {

        public ICategoriesDataStore DataStore => DependencyService.Get<ICategoriesDataStore>();
        public Recipe Recipe { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        private Category selectedCategory;
        public EditRecipeViewModel(Recipe recipe)
        {
            Title = recipe?.RecipeName;
            Recipe = recipe;
            DeserializeTags();
            DeleteTagCommand = new Command(tag => ExecuteDeleteTagCommand((string)tag));

            Category category = null;

            Categories = new ObservableCollection<Category>();
            if (category != null)
            {
                SelectedCategory = category;
            }

            GetCategories();
        }

        public void Save()
        {
            //Recipe.CategoryId = selectedCategory.CategoryId;
            Recipe.Tags = SerializeTags();
            MessagingCenter.Send(this, "EditRecipe", Recipe);
        }

        public void DeserializeTags()
        {
            string[] tags = Recipe.Tags.Split(',');
            foreach (string tag in tags)
            {
                Tags.Add(tag);
            }
        }

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set { SetProperty(ref selectedCategory, value); }
        }

        async void GetCategories()
        {
            Categories.Clear();
            var categories = await DataStore.GetItemsAsync(true);
            foreach (Category category in categories)
            {
                Categories.Add(category);
            }
        }
    }
}
