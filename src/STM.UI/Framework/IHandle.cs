// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IHandle.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework
{
    /// <summary>
    ///     A marker interface for classes that subscribe to messages.
    /// </summary>
    public interface IHandle
    {
    }

    // ReSharper disable TypeParameterCanBeVariant
    /// <summary>
    ///     Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name="TMessage">The type of message to handle.</typeparam>
    public interface IHandle<TMessage> : IHandle
    {
        /// <summary>
        ///     Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(TMessage message);
    }

    // ReSharper restore TypeParameterCanBeVariant
}
