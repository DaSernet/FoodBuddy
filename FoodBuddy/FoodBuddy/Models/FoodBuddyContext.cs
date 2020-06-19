using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Essentials;

namespace FoodBuddy.Models
{
    public class FoodBuddyContext : DbContext
    {

        private string connectionString;

        public FoodBuddyContext()
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "FoodBuddy.db3");
            connectionString = $"Filename={databasePath}";

            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString);
            }
        }
}
}
