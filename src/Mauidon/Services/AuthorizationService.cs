// <copyright file="AuthorizationService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Mastonet.Entities;
using Mauidon.Events;
using Mauidon.Models;

namespace Mauidon.Services
{
    /// <summary>
    /// Authorization Service.
    /// </summary>
    public class AuthorizationService
    {
        private string redirectUrl;
        private MauidonClient defaultClient;
        private string hostUrl = string.Empty;
        private AppRegistration? appRegistration;
        private AuthenticationClient? authClient;
        private IBrowserService browserService;
        private DatabaseContext databaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
        /// </summary>
        /// <param name="database">Database Context.</param>
        /// <param name="browserService">Browser Service.</param>
        /// <param name="redirectUri">Redirect URL.</param>
        public AuthorizationService(DatabaseContext database, IBrowserService browserService, string redirectUri = "urn:ietf:wg:oauth:2.0:oob")
        {
            this.databaseContext = database;
            this.browserService = browserService;
            this.redirectUrl = redirectUri;
            this.defaultClient = this.GetDefaultClient();
        }

        public event EventHandler<NewSignInEventArgs>? OnNewSignIn;

        /// <summary>
        /// Gets a value indicating whether gets a value indicating if the user needs to copy a code.
        /// </summary>
        public bool IsCodeAuth => this.redirectUrl == "urn:ietf:wg:oauth:2.0:oob";

        /// <summary>
        /// Gets the default Mauidon Client.
        /// </summary>
        public MauidonClient DefaultClient => this.defaultClient;

        /// <summary>
        /// Setup Login for Mastodon.
        /// </summary>
        /// <param name="serverBase">The base server login.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task SetupLogin(string serverBase)
        {
            this.appRegistration = await this.GetAppRegistrationAsync(serverBase);
            this.authClient = new AuthenticationClient(this.appRegistration);
            var oauthUrl = this.authClient.OAuthUrl(this.redirectUrl);
            await this.browserService.OpenAsync(oauthUrl);
        }

        /// <summary>
        /// Login with Mastodon OAuth Code.
        /// </summary>
        /// <param name="code">Code to Login.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task<MauidonClient> LoginWithCodeAsync(string code)
        {
            Auth? auth = await this.authClient!.ConnectWithCode(code, this.redirectUrl);
            var client = new MastodonClient(this.appRegistration!.Instance, auth.AccessToken);
            var account = await client.GetCurrentUser();
            var mauidonAccount = this.databaseContext.MauidonAccounts!.FirstOrDefault(n => n.AccountName == account.AccountName) ?? new MauidonAccount();
            mauidonAccount.Update(account, auth);
            this.databaseContext.AddOrUpdateMauidonAccount(mauidonAccount).FireAndForgetSafeAsync();
            return new MauidonClient(client, mauidonAccount);
        }

        /// <summary>
        /// Login with existing Mauidon Account.
        /// </summary>
        /// <param name="mauidonAccount">Mauidon Account.</param>
        /// <returns>New Mauidon Client.</returns>
        public MauidonClient LoginWithMauidonAccount(MauidonAccount mauidonAccount)
        {
            var client = new MastodonClient(mauidonAccount.Instance, mauidonAccount.AccessToken);
            this.defaultClient = new MauidonClient(client, mauidonAccount);
            this.OnNewSignIn?.Invoke(this, new NewSignInEventArgs(this.defaultClient));
            return this.defaultClient;
        }

        /// <summary>
        /// Gets the default client.
        /// </summary>
        /// <returns>MauidonClient.</returns>
        public MauidonClient GetDefaultClient()
        {
            MauidonClient mauidonClient;
            var account = this.databaseContext.MauidonAccounts!.FirstOrDefault(n => n.IsDefault);
            if (account is null)
            {
                mauidonClient = this.GenerateDefaultClient();
            }
            else
            {
                var client = new MastodonClient(account.Instance, account.AccessToken);
                mauidonClient = new MauidonClient(client, account);
            }

            this.defaultClient = mauidonClient;
            return mauidonClient;
        }

        /// <summary>
        /// Generates the default client.
        /// </summary>
        /// <returns>Mastodon Client.</returns>
        public MauidonClient GenerateDefaultClient()
        {
            return new MauidonClient(new MastodonClient("mastodon.social", string.Empty));
        }

        private async Task<AppRegistration> GetAppRegistrationAsync(string serverBase)
        {
            Uri.TryCreate(serverBase, UriKind.RelativeOrAbsolute, out Uri? serverBaseUri);
            if (serverBaseUri == null)
            {
                throw new InvalidServerUriException(serverBase);
            }

            this.hostUrl = serverBaseUri.IsAbsoluteUri ? serverBaseUri.Host : serverBaseUri.OriginalString;
            AppRegistration appRegistration;
            try
            {
                var initAuthClient = new AuthenticationClient(this.hostUrl);
                appRegistration = await initAuthClient.CreateApp("Mauidon", Scope.Read | Scope.Write | Scope.Follow, null, this.redirectUrl);
            }
            catch (Exception ex)
            {
                throw new AppRegistrationCreationFailureException(ex.Message, ex);
            }

            return appRegistration;
        }
    }
}
