// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.Logging;

namespace NuGet.Services.ServiceBus
{
    /// <summary>
    /// SubscriptionProcessor that completes and sends follow-up message in a transaction.
    /// Expects that underlying message handler returns a Func that when called sends a message to a
    /// service bus topic.
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class TransactionalCompleteAndSendSubscriptionProcessor<TMessage> : BaseSubscriptionProcessor<TMessage, Func<Task>>
    {
        public TransactionalCompleteAndSendSubscriptionProcessor(
            ISubscriptionClient client,
            IBrokeredMessageSerializer<TMessage> serializer,
            IBaseMessageHandler<TMessage, Func<Task>> handler,
            ISubscriptionProcessorTelemetryService telemetryService,
            ILogger<BaseSubscriptionProcessor<TMessage, Func<Task>>> logger)
            : base(
                  client,
                  serializer,
                  handler,
                  telemetryService,
                  logger)
        {

        }
        protected override bool IsSuccess(Func<Task> handleResult) => handleResult != null;

        internal override async Task CompleteMessageAsync(Func<Task> handleResult, IBrokeredMessage brokeredMessage)
        {
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await brokeredMessage.CompleteAsync();
                await handleResult();
            }
        }
    }
}
