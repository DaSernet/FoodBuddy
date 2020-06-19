using FoodBuddy.Models;
using FoodBuddy.Services.Interfaces;
using FoodBuddy.ViewModels.Categories;
using FoodBuddy.Views.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel
    {

        public ICategoriesDataStore DataStore => DependencyService.Get<ICategoriesDataStore>();
        public ObservableCollection<Category> Categories { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command PerformSearch { get; set; }

        public CategoriesViewModel()
        {
            Title = "FoodBuddy";
            Categories = new ObservableCollection<Category>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SortCommand = new Command(async (parameter) => await ExecuteLoadItemsCommand((string)parameter));
            PerformSearch = new Command(async (search) => await ExecutePerformSearchCommand((string)search));

            MessagingCenter.Subscribe<NewCategoryViewModel, Category>(this, "AddCategory", async (obj, category) =>
            {
                Categories.Add(category);
                await DataStore.AddItemAsync(category);
            });

            MessagingCenter.Subscribe<CategoryDetailViewModel, Category>(this, "DeleteCategory", async (obj, category) =>
            {
                await DeleteCategory(category);
            });
        }

        public async Task DeleteCategory(Category category)
        {
            Categories.Remove(category);
            await DataStore.DeleteItemAsync(category.CategoryId);
        }

        async Task ExecuteLoadItemsCommand(string sorter = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await DataStore.GetItemsAsync(true, sorter);
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task ExecutePerformSearchCommand(string search, string sorter = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await DataStore.GetItemsByTagsAsync(search, sorter);
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
