// <copyright file="AppRegistrationCreationFailureException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauidon
{
    /// <summary>
    /// App Registration Creation Failure Exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Exception File.")]
    public class AppRegistrationCreationFailureException : AuthorizationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppRegistrationCreationFailureException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="ex">Exception.</param>
        public AppRegistrationCreationFailureException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
