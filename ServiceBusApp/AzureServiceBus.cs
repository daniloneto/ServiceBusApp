// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceBusApp.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusApp
{
    public class AzureServiceBus : IAzureServiceBus
    {
        private static AutoResetEvent s_waitHandle = new AutoResetEvent(false);

        public async Task<bool> PostToServiceBus(string connectionString, string serviceBusQueue, string msgJson)
        {
          
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {                
                ServiceBusSender sender = client.CreateSender(serviceBusQueue);
             
                ServiceBusMessage message = new ServiceBusMessage(msgJson);
                
                try
                {                    
                    await sender.SendMessageAsync(message);                                     
                }
                catch (Exception ex)
                {

                    //log exception and return false
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }            
            return true;
        }

        public void ListeningServiceBus(string connectionString, string serviceBusQueue)
        {
            try
            {
                //configura o cancelkeypress para liberar a thread
                Console.CancelKeyPress += (o, e) =>
                {
                    Console.WriteLine("Exit...");

                    // Libera a continuação da thread principal
                    s_waitHandle.Set();
                };
               
                ServiceBusClient client = new ServiceBusClient(connectionString);             

                ServiceBusProcessor processor = client.CreateProcessor(serviceBusQueue, new ServiceBusProcessorOptions());

                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                Console.WriteLine("Start Listening");
                processor.StartProcessingAsync();

                s_waitHandle.WaitOne();             
                processor.StopProcessingAsync();

                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            await args.CompleteMessageAsync(args.Message);
            IEmailEntity packageEmailSend = JsonConvert.DeserializeObject<IEmailEntity>(body);
            if (packageEmailSend.emailTo != null)
            {
                Console.WriteLine("Email found.");
            }
            else
            {
                Console.WriteLine("No Email found");
            }

        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
