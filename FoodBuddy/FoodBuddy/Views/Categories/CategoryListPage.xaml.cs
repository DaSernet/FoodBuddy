using FoodBuddy.Models;
using FoodBuddy.ViewModels;
using FoodBuddy.ViewModels.Categories;
using FoodBuddy.Views.Categories;
using FoodBuddy.Views.TabbedPages;
using FoodBuddy.ViewModels.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodBuddy.Views.Categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryListPage : ContentPage
    {

        CategoriesViewModel viewModel;

        public CategoryListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoriesViewModel();

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var category = (Category)e.Item;
            await Navigation.PushAsync(new CategoryTabbedPage(new CategoryTabbedViewModel(category)));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void AddCategoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCategoryPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Categories.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = (Category)menuItem.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new EditCategoryPage(new EditCategoryViewModel(contextItem))));
        }

        private async void DetailsButton_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = (Category)menuItem.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new CategoryDetailPage(new CategoryDetailViewModel(contextItem))));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Save changes?", "Yes", "No");
            if (answer == true)
            {
                MenuItem menuItem = sender as MenuItem;
                var contextItem = (Category)menuItem.BindingContext;
                await viewModel.DeleteCategory(contextItem);
            }
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            SearchBar.Text = null;
            SearchBar.IsVisible = !SearchBar.IsVisible;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            await viewModel.ExecutePerformSearchCommand(e.NewTextValue);
        }
    }
}