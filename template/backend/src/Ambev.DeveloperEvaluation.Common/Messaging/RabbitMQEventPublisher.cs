using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace Ambev.DeveloperEvaluation.Common.Messaging
{
    /// <summary>
    /// Implements the IEventPublisher interface using RabbitMQ.
    /// </summary>
    public class RabbitMQEventPublisher : IEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchange;
        private readonly string _routingKey;
        private readonly string _queueName = "sales_queue";

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQEventPublisher"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration to read RabbitMQ settings.</param>
        public RabbitMQEventPublisher(IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:HostName"],
                Port = int.Parse(configuration["RabbitMQ:Port"]),
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };

            _exchange = configuration["RabbitMQ:Exchange"] ?? "sales_exchange";
            _routingKey = configuration["RabbitMQ:RoutingKey"] ?? "sales_event";

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.ExchangeDeclare(_exchange, ExchangeType.Direct, durable: true);

            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchange, routingKey: _routingKey);
        }

        /// <summary>
        /// Publishes an event to RabbitMQ.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="payload">The event payload.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        public Task PublishEventAsync(string eventName, object payload, CancellationToken cancellationToken)
        {
            // Combine the event name with the routing key if needed
            var message = new
            {
                Event = eventName,
                Data = payload
            };

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: _exchange,
                                  routingKey: _routingKey,
                                  basicProperties: null,
                                  body: body);

            return Task.CompletedTask;
        }
    }
}
