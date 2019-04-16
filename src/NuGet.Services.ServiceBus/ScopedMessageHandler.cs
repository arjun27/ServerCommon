// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace NuGet.Services.ServiceBus
{
    public class ScopedMessageHandler<TMessage> : BaseScopedMessageHandler<TMessage, bool>,
        IMessageHandler<TMessage>
    {
        public ScopedMessageHandler(IServiceScopeFactory scopeFactory)
            : base(scopeFactory)
        {

        }
    }
}
