// <copyright file="AppSettings.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace Mauidon.Models
{
    /// <summary>
    /// App Settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the last updated time of the app.
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the app theme.
        /// </summary>
        public AppTheme AppTheme { get; set; }

        /// <summary>
        /// Gets or sets the language setting.
        /// </summary>
        public LanguageSetting LanguageSetting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to turn on timeline streams by default.
        /// </summary>
        public bool TurnOnTimelineStreams { get; set; } = true;
    }
}
