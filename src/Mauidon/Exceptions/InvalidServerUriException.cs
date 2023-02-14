// <copyright file="InvalidServerUriException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauidon
{
    /// <summary>
    /// Invalid Server Uri Exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Exception File.")]
    public class InvalidServerUriException : AuthorizationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidServerUriException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public InvalidServerUriException(string message)
            : base(message)
        {
        }
    }
}
