using FoodBuddy.Models;
using FoodBuddy.ViewModels;
using FoodBuddy.ViewModels.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodBuddy.Views.Recipes;

namespace FoodBuddy.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryTabbedPage : TabbedPage
    {
        CategoryTabbedViewModel viewModel;
        public CategoryTabbedPage(CategoryTabbedViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            this.Children.Add(new RecipesListPage(viewModel.RecipesViewModel));
        }
    }
}