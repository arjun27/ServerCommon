// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace NuGet.Services.ServiceBus
{
    /// <summary>
    /// The interface for the class that handles messages received by <see cref="TransactionalCompleteAndSendSubscriptionProcessor{TMessage}"/>.
    /// Typical usage is for transactional completion and sending of follow-up message.
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface ITransactionalCompleteAndSendMessageHandler<TMessage> : IBaseMessageHandler<TMessage, Func<Task>>
    {
    }
}
