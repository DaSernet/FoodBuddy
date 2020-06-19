using FoodBuddy.Models;
using FoodBuddy.ViewModels;
using FoodBuddy.ViewModels.Recipes;
using FoodBuddy.Views.Recipes;
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
    public partial class RecipesListPage : ContentPage
    {
        RecipesViewModel viewModel;

        public RecipesListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RecipesViewModel();
        }

        public RecipesListPage(RecipesViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var recipe = (Recipe)e.Item;
            await Navigation.PushModalAsync(new NavigationPage(new RecipeDetailPage(new RecipeDetailViewModel(recipe))));

            ((ListView)sender).SelectedItem = null;
        }

        private async void AddRecipeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewRecipePage(viewModel.category)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Recipes.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void EditMenuButton_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = (Recipe)menuItem.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new EditRecipePage(new EditRecipeViewModel(contextItem))));
        }

        private async void DeleteMenuButton_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = (Recipe)menuItem.BindingContext;
            await viewModel.DeleteRecipe(contextItem);
        }

        private async void FavoriteMenuButton_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = (Recipe)menuItem.BindingContext;
            contextItem.RecipeFavorited = !contextItem.RecipeFavorited;
            await viewModel.UpdateRecipe(contextItem);
            
        }

        private void SearchToolbarButton_Clicked(object sender, EventArgs e)
        {
            SearchBar.Text = null;
            SearchBar.IsVisible = !SearchBar.IsVisible;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            await viewModel.ExecutePerformSearchCommand(e.NewTextValue, viewModel.category);
        }
    }
}