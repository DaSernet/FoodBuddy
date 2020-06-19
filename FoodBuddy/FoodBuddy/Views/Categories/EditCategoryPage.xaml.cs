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
    public partial class EditCategoryPage : ContentPage
    {
        EditCategoryViewModel viewModel;
        public EditCategoryPage(EditCategoryViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Save changes?", "Yes", "No");
            if (answer == true)
            {
                viewModel.Save();
                await Navigation.PopModalAsync();
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
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