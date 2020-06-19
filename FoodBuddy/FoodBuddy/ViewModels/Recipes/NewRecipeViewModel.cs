using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Recipes
{
    public class NewRecipeViewModel : BaseViewModel
    {
        public ICategoriesDataStore DataStore => DependencyService.Get<ICategoriesDataStore>();
        public Recipe Recipe { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        private Category selectedCategory;

        public NewRecipeViewModel(Category category = null)
        {
            Recipe = new Recipe();
            Categories = new ObservableCollection<Category>();

            if (category != null)
            {
                SelectedCategory = category;
            }

            GetCategories();
            DeleteTagCommand = new Command(tag => ExecuteDeleteTagCommand((string)tag));
        }

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set { SetProperty(ref selectedCategory, value); }
        }

        public void Save()
        {
            Recipe.CategoryId = selectedCategory.CategoryId;
            Recipe.Tags = SerializeTags();
            MessagingCenter.Send(this, "AddRecipe", Recipe);
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
