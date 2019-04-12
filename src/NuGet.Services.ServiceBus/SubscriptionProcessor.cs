// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NuGet.Services.ServiceBus
{
    /// <summary>
    /// Basic subscription processor that does not do anything special.
    /// </summary>
    /// <typeparam name="TMessage">Type of the message to handle.</typeparam>
    public class SubscriptionProcessor<TMessage> : BaseSubscriptionProcessor<TMessage, bool>
    {
        public SubscriptionProcessor(
            ISubscriptionClient client,
            IBrokeredMessageSerializer<TMessage> serializer,
            IMessageHandler<TMessage> handler,
            ISubscriptionProcessorTelemetryService telemetryService,
            ILogger<SubscriptionProcessor<TMessage>> logger)
            : base(
                  client,
                  serializer,
                  handler,
                  telemetryService,
                  logger)
        {

        }

        protected override bool IsSuccess(bool handleResult) => handleResult;

        internal override async Task CompleteMessageAsync(bool handleResult, IBrokeredMessage brokeredMessage)
        {
            await brokeredMessage.CompleteAsync();
        }
    }
}
