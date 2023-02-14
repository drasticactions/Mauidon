// <copyright file="MauidonAccount.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet.Entities;
using Newtonsoft.Json;

namespace Mauidon.Models
{
    public class MauidonAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonAccount"/> class.
        /// </summary>
        public MauidonAccount()
        {
        }

        /// <summary>
        /// Gets or sets the Id of the Mauidon Account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the username of the account, not including domain.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the Webfinger account URI. Equal to username for local users, or username@domain for remote users.
        /// </summary>
        public string AccountName { get; set; } = string.Empty;

        /// <summary>
        /// Gets a static version of the avatar. Equal to avatar if its value is a static image; different if avatar is an animated GIF.
        /// </summary>
        [JsonProperty("avatar_static")]
        public string StaticAvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scope of the token.
        /// </summary>
        public string Scope { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the time the token was created at.
        /// </summary>
        public string CreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to set this account as the default.
        /// </summary>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Gets or sets the instance for the user.
        /// </summary>
        public string Instance { get; set; } = string.Empty;

        /// <summary>
        /// Update the given Mauidon Account.
        /// </summary>
        /// <param name="account">Account.</param>
        /// <param name="auth">Auth.</param>
        public void Update(Account account, Auth auth)
        {
            this.AccountName = account.AccountName;
            this.StaticAvatarUrl = account.StaticAvatarUrl;
            this.UserName = account.UserName;
            this.AccessToken = auth.AccessToken;
            this.Scope = auth.Scope;
            this.CreatedAt = auth.CreatedAt;
            this.TokenType = auth.TokenType;
        }
    }
}
