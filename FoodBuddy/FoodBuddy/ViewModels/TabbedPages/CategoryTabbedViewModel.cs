using FoodBuddy.Models;
using FoodBuddy.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodBuddy.ViewModels.TabbedPages
{
    public class CategoryTabbedViewModel : BaseViewModel
    {
        public RecipesViewModel RecipesViewModel { get; set; }

        public CategoryTabbedViewModel(Category category)
        {
            Title = category.CategoryName;

            RecipesViewModel = new RecipesViewModel(category);
        }
    }
}
