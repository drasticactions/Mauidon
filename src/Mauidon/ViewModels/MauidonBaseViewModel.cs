// <copyright file="MauidonBaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;

namespace Mauidon.ViewModels
{
    public class MauidonBaseViewModel : BaseViewModel
    {
        public MauidonBaseViewModel(IServiceProvider services)
            : base(services)
        {
            this.Client = services.GetService(typeof(MastodonClient)) as MastodonClient ?? throw new NullReferenceException(nameof(MastodonClient));
        }

        /// <summary>
        /// Gets the <see cref="MastodonClient"/>.
        /// </summary>
        internal MastodonClient Client { get; }
    }
}
