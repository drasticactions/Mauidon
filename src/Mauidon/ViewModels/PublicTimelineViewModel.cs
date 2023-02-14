// <copyright file="PublicTimelineViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauidon.Models;

namespace Mauidon.ViewModels
{
    /// <summary>
    /// Public Timeline View Model.
    /// </summary>
    public class PublicTimelineViewModel : TimelineViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicTimelineViewModel"/> class.
        /// </summary>
        /// <param name="services">Services.</param>
        public PublicTimelineViewModel(IServiceProvider services)
            : base(MastodonTimelineType.Public, services)
        {
        }
    }
}
