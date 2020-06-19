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
    public partial class EditRecipePage : ContentPage
    {
        EditRecipeViewModel viewModel;
        public EditRecipePage(EditRecipeViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Save changes?", "Yes", "No");
            if (answer == true)
            {
                viewModel.Save();
                await Navigation.PopModalAsync();
            }
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