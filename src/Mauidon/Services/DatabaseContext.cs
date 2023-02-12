// <copyright file="DatabaseContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauidon.Models;
using Microsoft.EntityFrameworkCore;

namespace Mauidon.Services
{
    /// <summary>
    /// Database Context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        private string databasePath = "database.db";

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="databasePath">Path to database.</param>
        public DatabaseContext(string databasePath = "")
        {
            if (!string.IsNullOrEmpty(databasePath))
            {
                this.databasePath = databasePath;
            }

            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets the database Path.
        /// </summary>
        public string Location => this.databasePath;

        /// <summary>
        /// Gets or sets the App Settings.
        /// </summary>
        public DbSet<AppSettings>? AppSettings { get; set; }

        /// <summary>
        /// Run when configuring the database.
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/>.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={this.databasePath}");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /// <summary>
        /// Run when building the model.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<AppSettings>().HasKey(n => n.Id);
        }
    }
}
