using Newtonsoft.Json;
using ServiceBusApp.Interfaces;
using System;

namespace ServiceBusApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var connectionString = "Endpoint=sb://yourazure.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=342iojdfdsoij3df";
            //var serviceBusName = "your-azure-queue";
            var connectionString = "Endpoint=sb://jdmnbus01.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=MC6W/5ivuNMe9hmkoyv1N3iAKffEUStd/sKfyxxYX9w=";
            var serviceBusName = "dev-email-queue";

            AzureServiceBus azureServiceBus = new AzureServiceBus();
            IEmailEntity emailEntity = new IEmailEntity();

            emailEntity.title = "Test Send JSON to ServiceBus";
            emailEntity.body = "Body Mail";
            emailEntity.emailTo = "email@gmail.com";

            //send 10 messages to service bus;
            for (int i = 0; i < 10; i++)
            {
                azureServiceBus.PostToServiceBus(connectionString, serviceBusName, JsonConvert.SerializeObject(emailEntity));
            }        

            Console.WriteLine("Press any key to continue.");
            Console.Read();

            azureServiceBus.ListeningServiceBus(connectionString, serviceBusName);

            

        }
    }
}
