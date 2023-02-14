// <copyright file="NewSignInEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauidon.Models;

namespace Mauidon.Events
{
    /// <summary>
    /// New Sign in Event Args.
    /// </summary>
    public class NewSignInEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewSignInEventArgs"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        public NewSignInEventArgs(MauidonClient client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Gets the Mauidon Client.
        /// </summary>
        public MauidonClient Client { get; }
    }
}
