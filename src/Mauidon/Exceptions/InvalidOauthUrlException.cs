// <copyright file="InvalidOauthUrlException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauidon
{
    /// <summary>
    /// Invalid Oauth Url Exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Exception File.")]
    public class InvalidOauthUrlException : AuthorizationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOauthUrlException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="ex">Exception.</param>
        public InvalidOauthUrlException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
