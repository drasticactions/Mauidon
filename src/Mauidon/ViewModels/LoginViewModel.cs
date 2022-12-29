// <copyright file="LoginViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauidon.ViewModels
{
    public class LoginViewModel : MauidonBaseViewModel
    {
        private string serverBaseUrl = "mastodon.social";
        private string code = string.Empty;

        public LoginViewModel(IServiceProvider services)
            : base(services)
        {
             this.StartLoginCommand = new AsyncCommand(
                async () => await this.ExecuteStartLoginCommand(),
                () => !string.IsNullOrEmpty(this.ServerBaseUrl),
                this.Dispatcher,
                this.ErrorHandler);

            this.StartAuthViaCodeCommand = new AsyncCommand(
                async () => await this.PerformBusyAsyncTask(this.ExecuteStartAuthViaCodeCommand),
                () => !string.IsNullOrEmpty(this.ServerBaseUrl) && !string.IsNullOrEmpty(this.Code),
                this.Dispatcher,
                this.ErrorHandler);
        }

        /// <summary>
        /// Gets or sets the server base url.
        /// </summary>
        public string ServerBaseUrl
        {
            get => this.serverBaseUrl;
            set
            {
                this.SetProperty(ref this.serverBaseUrl, value);
                this.StartLoginCommand?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code
        {
            get => this.code;
            set
            {
                this.SetProperty(ref this.code, value);
                this.StartAuthViaCodeCommand?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the Start Login Command.
        /// </summary>
        public AsyncCommand StartLoginCommand { get; }

        /// <summary>
        /// Gets the Start Auth Via Code Command.
        /// </summary>
        public AsyncCommand StartAuthViaCodeCommand { get; }

        private async Task ExecuteStartAuthViaCodeCommand()
        {
            await this.Authorization.LoginWithCodeAsync(this.Code);
        }

        private async Task ExecuteStartLoginCommand()
        {
            if (!string.IsNullOrEmpty(this.ServerBaseUrl))
            {
                await this.Authorization.SetupLogin(this.ServerBaseUrl);
            }
        }
    }
}
