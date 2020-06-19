using FoodBuddy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodBuddy.ViewModels.Categories
{
    public class EditCategoryViewModel : BaseViewModel
    {
        public Category Category { get; set; }
        public EditCategoryViewModel(Category category)
        {
            Category = category;
            Title = category?.CategoryName;
            DeserializeTags();
            DeleteTagCommand = new Command(tag => ExecuteDeleteTagCommand((string)tag));
        }

        public void Save()
        {
            Category.Tags = SerializeTags();
            MessagingCenter.Send(this, "EditCategory", Category);
        }

        public void DeserializeTags()
        {
            string[] tags = Category.Tags.Split(',');
            foreach (string tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}
