using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Categories
{
    public class CategoryDetailViewModel : BaseViewModel
    {
        public ICategoriesDataStore DataStore => DependencyService.Get<ICategoriesDataStore>();
        public Category Category { get; set; }

        public CategoryDetailViewModel(Category category = null)
        {
            Title = category?.CategoryName;
            Category = category;
            DeserializeTags();

            MessagingCenter.Subscribe<EditCategoryViewModel, Category>(this, "EditCategory", async (obj, update) =>
            {
                Category = update;
                Title = update.CategoryName;
                DeserializeTags();
                await DataStore.UpdateItemAsync(update);
            });
        }

        public void Delete()
        {
            MessagingCenter.Send(this, "DeleteCategory", Category);
        }

        public void DeserializeTags()
        {
            Tags.Clear();
            string[] tags = Category.Tags.Split(',');
            foreach (string tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}
