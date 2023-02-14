// <copyright file="MauidonClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Mastonet.Entities;

namespace Mauidon.Models
{
    /// <summary>
    /// Mauidon Client.
    /// </summary>
    public class MauidonClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonClient"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="account">Account.</param>
        public MauidonClient(MastodonClient client, MauidonAccount? account = default)
        {
            this.Client = client;
            this.Account = account;
        }

        /// <summary>
        /// Gets the authenticated mastodon client.
        /// This may be a logged in instance for a user, or a "demo" account used to populate fields.
        /// </summary>
        public MastodonClient Client { get; }

        /// <summary>
        /// Gets the logged in account, if logged in.
        /// </summary>
        public MauidonAccount? Account { get; }

        /// <summary>
        /// Gets a value indicating whether the account is logged in.
        /// </summary>
        public bool IsLoggedIn => this.Account is not null;
    }
}
