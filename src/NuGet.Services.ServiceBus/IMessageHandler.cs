// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace NuGet.Services.ServiceBus
{
    /// <summary>
    /// The interface for the class that handles the messages received by 
    /// Typical usage is for the case when completing the message and sending of the follow-up message (if it even happens)
    /// are completely unrelated.
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMessageHandler<TMessage> : IBaseMessageHandler<TMessage, bool>
    {
    }
}
