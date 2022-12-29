// <copyright file="MauidonBaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Mauidon.Services;

namespace Mauidon.ViewModels
{
    public class MauidonBaseViewModel : BaseViewModel
    {
        public MauidonBaseViewModel(IServiceProvider services)
            : base(services)
        {
            this.Authorization = services.GetService(typeof(IAuthorizationService)) as IAuthorizationService ?? throw new NullReferenceException(nameof(IAuthorizationService));
        }

        /// <summary>
        /// Gets the <see cref="IAuthorizationService"/>.
        /// </summary>
        internal IAuthorizationService Authorization { get; }
    }
}
