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
            this.Authorization = services.GetService(typeof(AuthorizationService)) as AuthorizationService ?? throw new NullReferenceException(nameof(AuthorizationService));
        }

        /// <summary>
        /// Gets the <see cref="IAuthorizationService"/>.
        /// </summary>
        internal AuthorizationService Authorization { get; }
    }
}
