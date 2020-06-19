using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using FoodBuddy.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodBuddy.Views.Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecipePage : ContentPage
    {

        NewRecipeViewModel viewModel;

        public NewRecipePage(Category category = null)
        {
            InitializeComponent();

            BindingContext = viewModel = new NewRecipeViewModel(category);

        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            viewModel.Save();
            await Navigation.PopModalAsync();
        }

        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void AddTagButton_Clicked(object sender, EventArgs e)
        {
            string tag = TagEntry.Text.Trim();
            viewModel.AddTag(tag);
            TagEntry.Text = null;
        }
    }
}