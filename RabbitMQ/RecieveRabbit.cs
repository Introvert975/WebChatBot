using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace WebChatBot.RabbitMQ
{
    internal class ReceiveRabbit : IDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        public ReceiveRabbit(string hostName, string queueName)
        {
            _factory = new ConnectionFactory() { HostName = hostName };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;

            _channel.QueueDeclare(queue: _queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void ReceiveMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.Span);
                Console.WriteLine("Received message: {0}", message);
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Close();
        }
    }
}
