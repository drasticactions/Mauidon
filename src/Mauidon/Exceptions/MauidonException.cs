// <copyright file="MauidonException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Runtime.Serialization;

namespace Mauidon
{
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
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Context.</param>
        public MauidonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public MauidonException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MauidonException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="ex">Exception.</param>
        public MauidonException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
