using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace RabbitMQServiceApp
{
    public interface IRabbitManager
    {
        void Publish<T>(T message, string routeKey, Action<IBasicProperties> configureProps)
            where T : class;

        void SetExchange(string exchangeName, string exchangeType, bool isDurable, bool autoDelete);
    }

}
