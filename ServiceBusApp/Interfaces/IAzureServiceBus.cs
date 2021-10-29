// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System.Threading.Tasks;

namespace ServiceBusApp.Interfaces
{
    public interface IAzureServiceBus
    {
        Task<bool> PostToServiceBus(string serviceBusConnectionString, string serviceBusQueue, string msgJson);

        void ListeningServiceBus(string connectionString, string serviceBusQueue);
    }
}
