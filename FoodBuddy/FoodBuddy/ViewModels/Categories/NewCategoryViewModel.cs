using FoodBuddy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Categories
{
    public class NewCategoryViewModel : BaseViewModel
    {
        public Category Category { get; set; }
        public NewCategoryViewModel()
        {
            Category = new Category();
            DeleteTagCommand = new Command(tag => ExecuteDeleteTagCommand((string)tag));
        }

        public void Save()
        {
            Category.Tags = SerializeTags();
            MessagingCenter.Send(this, "AddCategory", Category);
        }
    }
}
