

namespace productorPp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using productorPp.Models;
    using Newtonsoft.Json;
    [Route("api/[controller]")]
    [ApiController]
    public class OdometroController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Odometro odometro)
        {
             string connectionString = "Endpoint=sb://queuepreparcial.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=WBojAVAqVi1QDuznkNaUnl5z11PzvqjHlu76Px02iC4=;EntityPath=colapp";
              string queueName = "colapp";

            string Mensaje = JsonConvert.SerializeObject(odometro);
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(Mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        }
    }
}
