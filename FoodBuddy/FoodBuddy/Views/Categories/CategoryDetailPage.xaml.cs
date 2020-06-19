using FoodBuddy.Models;
using FoodBuddy.ViewModels.Categories;
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
    public partial class CategoryDetailPage : ContentPage
    {
        CategoryDetailViewModel viewModel;
        public CategoryDetailPage(CategoryDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void EditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditCategoryPage(new EditCategoryViewModel(viewModel.Category))));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            viewModel.Delete();
            await Navigation.PopModalAsync();
        }
    }
}