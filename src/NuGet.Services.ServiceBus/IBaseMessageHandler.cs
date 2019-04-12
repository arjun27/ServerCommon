// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace NuGet.Services.ServiceBus
{
    /// <summary>
    /// The interface for the class that handles messages received by <see cref="BaseSubscriptionProcessor{TMessage, TResult}"/>
    /// </summary>
    /// <typeparam name="TMessage">The type of messages this handler handles.</typeparam>
    /// <typeparam name="TResult">The type of the result of handing the message.</typeparam>
    public interface IBaseMessageHandler<TMessage, TResult>
    {
        /// <summary>
        /// Handle the message.
        /// </summary>
        /// <param name="message">The received message.</param>
        /// <returns>Whether the message has been handled. If false, the message will be requeued to be handled again later.</returns>
        Task<TResult> HandleAsync(TMessage message);
    }
}
