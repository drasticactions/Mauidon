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
        /// Gets or sets the Mauidon Accounts.
        /// </summary>
        public DbSet<MauidonAccount>? MauidonAccounts { get; set; }

        /// <summary>
        /// Add or update Mauidon Account.
        /// </summary>
        /// <param name="account">Mastodon Account.</param>
        /// <returns>Number of rows changed.</returns>
        public async Task<int> AddOrUpdateMauidonAccount(MauidonAccount account)
        {
            if (account.Id <= 0)
            {
                await this.MauidonAccounts!.AddAsync(account);
            }
            else
            {
                this.MauidonAccounts!.Update(account);
            }

            return await this.SaveChangesAsync();
        }

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
            modelBuilder.Entity<MauidonAccount>().HasKey(n => n.Id);
        }
    }
}
