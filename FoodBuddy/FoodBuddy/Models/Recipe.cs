using System;
using System.Collections.Generic;
using System.Text;

namespace FoodBuddy.Models
{
    public class Recipe : BaseModel
    {
        //RecipeId
        //Primary key, auto incrementing
        private int recipeId;
        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty(ref recipeId, value); }
        }

        //RecipeName
        private string recipeName = string.Empty;
        public string RecipeName
        {
            get { return recipeName; }
            set { SetProperty(ref recipeName, value); }
        }

        //RecipeDuration
        private int? recipeDuration;
        public int? RecipeDuration
        {
            get { return recipeDuration; }
            set { SetProperty(ref recipeDuration, value); }
        }

        //RecipeFavorited
        private bool recipeFavorited = false;
        public bool RecipeFavorited
        {
            get { return recipeFavorited; }
            set { SetProperty(ref recipeFavorited, value); }
        }

        //RecipePrepare
        private string recipePrepare = string.Empty;
        public string RecipePrepare
        {
            get { return recipePrepare; }
            set { SetProperty(ref recipePrepare, value); }
        }

        //RecipeRating
        private double? recipeRating;
        public double? RecipeRating
        {
            get { return recipeRating; }
            set { SetProperty(ref recipeRating, value); }
        }

        //what CategoryId is our Recipe in?
        //Foreign key
        private int categoryId;
        public int  CategoryId
        {
            get { return categoryId; }
            set { SetProperty(ref categoryId, value); }
        }
        public Category Category { get; set; }
    }
}
