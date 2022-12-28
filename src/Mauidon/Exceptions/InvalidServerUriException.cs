// <copyright file="InvalidServerUriException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Runtime.Serialization;

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
            : base(message) { }
    }

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
            : base(message, ex) { }
    }

    /// <summary>
    /// App Registeration Creation Faulure Exception.
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
            : base(message, ex) { }
    }

    /// <summary>
    /// Authorization Exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Exception File.")]
    public class AuthorizationException : MauidonException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public AuthorizationException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="ex">Exception.</param>
        public AuthorizationException(string message, Exception ex)
            : base(message, ex) { }
    }

    /// <summary>
    /// Mauidon Exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Exception File.")]
    public class MauidonException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        public MauidonException()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Context.</param>
        public MauidonException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public MauidonException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="ex">Exception.</param>
        public MauidonException(string message, Exception ex)
            : base(message, ex) { }
    }
}
