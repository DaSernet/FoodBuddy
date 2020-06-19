using System;
using System.Collections.Generic;
using System.Text;

namespace FoodBuddy.Models
{
    public class Category : BaseModel
    {
        //CategoryId
        //Primary key => auto incrementing
        private int categoryId;
        public int CategoryId
        {
            get { return categoryId; }
            set { SetProperty(ref categoryId, value); }
        }

        //CategoryName
        private string categoryName = string.Empty;
        public string CategoryName
        {
            get { return categoryName; }
            set { SetProperty(ref categoryName, value); }
        }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
