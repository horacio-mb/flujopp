namespace fnConsumidorPp
{
    using fnConsumidorPp.Models;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync([ServiceBusTrigger("colapp", Connection = "MyConn")]string myQueueItem, 
            [CosmosDB(
                databaseName:"",
            collectionName:"",
            ConnectionStringSetting =""
            )]IAsyncCollector<object> datos,
            ILogger log)
        {
            try {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var data = JsonConvert.DeserializeObject<Odometro>(myQueueItem);
                await datos.AddAsync(data);
            } catch(Exception ex) {
                log.LogError($"No fue posible: {ex.Message}");
            }
            
        }
    }
}
