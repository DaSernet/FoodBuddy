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
    public partial class NewCategoryPage : ContentPage
    {
        NewCategoryViewModel viewModel;

        public NewCategoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewCategoryViewModel();
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

        private void addTagButton_Clicked(object sender, EventArgs e)
        {
            string tag = TagEntry.Text.Trim();
            viewModel.AddTag(tag);
            TagEntry.Text = null;
        }
    }
}