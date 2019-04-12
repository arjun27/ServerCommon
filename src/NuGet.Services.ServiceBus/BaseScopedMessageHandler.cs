// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NuGet.Services.ServiceBus
{
    /// <summary>
    /// Handles messages received by a <see cref="ISubscriptionProcessor{TMessage}"/>.
    /// Each message will be handled within its own dependency injection scope.
    /// </summary>
    /// <typeparam name="TMessage">The type of messages this handler handles.</typeparam>
    public class BaseScopedMessageHandler<TMessage, TResult> : IBaseMessageHandler<TMessage, TResult>
    {
        /// <summary>
        /// The factory used to create independent dependency injection scopes for each message.
        /// </summary>
        private readonly IServiceScopeFactory _scopeFactory;

        public BaseScopedMessageHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        /// <summary>
        /// Handle the message in its own dependency injection scope.
        /// </summary>
        /// <param name="message">The received message.</param>
        /// <returns>Whether the message has been handled. If false, the message will be requeued to be handled again later.</returns>
        public async Task<TResult> HandleAsync(TMessage message)
        {
            // Create a new scope for this message.
            using (var scope = _scopeFactory.CreateScope())
            {
                // Resolve a new message handler for the newly created scope and let it handle the message.
                return await ResolveMessageHandler(scope).HandleAsync(message);
            }
        }

        /// <summary>
        /// Resolve the message handler given a specific dependency injection scope.
        /// </summary>
        /// <param name="scope">The dependency injection scope that should be used to resolve services.</param>
        /// <returns>The resolved message handler service from the given scope.</returns>
        private IBaseMessageHandler<TMessage, TResult> ResolveMessageHandler(IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<IBaseMessageHandler<TMessage, TResult>>();
        }
    }
}
