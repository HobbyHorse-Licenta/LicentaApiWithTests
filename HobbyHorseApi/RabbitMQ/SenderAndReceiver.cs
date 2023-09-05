using RabbitMQ.Client;
using System.Text;
using HobbyHorseApi.Entities;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HobbyHorseApi.RabbitMQ
{
    public class SenderAndReceiver
    {
        IConnection _connection;
        IModel _receiveChannel = null;
        IModel _sendChannel = null;

        private static readonly string sendQueueName = "sendForCreation";
        private static readonly string receiveQueueName = "receiveForPosting";

        public SenderAndReceiver()
        {
           setupConnectionAndQueue();
        }
        
        public void ProcessReveivedJson(string json)
        {
            Console.WriteLine("I received FROM events generator: " + json);
   
        }
        public void SendPartialAggresiveEvent(AggresiveEvent aggresiveEvent)
        {

            string json = JsonSerializer.Serialize(aggresiveEvent);

           Console.WriteLine("I am sending a partial event: " + json);
            PublishString($"{Messages.createAggresiveEvent} " + json);
        }

        public void SendScheduleToBeAddedToExistingAggresiveEvents(Schedule schedule)
        {
            //set these to null to avoid cycles
            schedule.Zones[0].Schedule = null;
            schedule.Zones[0].Location.Zones = null;
            /////
            string json = JsonSerializer.Serialize(schedule);


            Console.WriteLine("I am sending a schedule to be added to aggresive event: " + json);
            PublishString($"{Messages.addScheduleToAggresiveEvents} " + json);
        }

        public void setupConnectionAndQueue()
        {
            _connection = RabbitMQConnection.GetConnection();
            _sendChannel = RabbitMQConnection.GetSenderChannel(sendQueueName);
            _receiveChannel = RabbitMQConnection.GetReceiverChannel(receiveQueueName);

            var consumer = new EventingBasicConsumer(_receiveChannel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                ProcessReveivedJson(json);
            };

            _receiveChannel.BasicConsume(queue: receiveQueueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void PublishString(string message)
        {
            if(_sendChannel != null)
            {
                Console.WriteLine("sending channel that is sending: " + _sendChannel.CurrentQueue);

                var body = Encoding.UTF8.GetBytes(message);
                _sendChannel.BasicPublish(exchange: "",
                                     routingKey: sendQueueName,
                                     basicProperties: null,
                                     body: body);
            }
            else Console.WriteLine("No sending channel");
         
        }
    }
}
