using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQServiceApp.Exceptions;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQServiceApp
{
    public abstract class RabbitManager : IRabbitManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;
        private string _exchnageName;
        private string _exchangeType;
        private bool _isDurable;
        private bool _autoDelete; 

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }

        // RPC methods
        public abstract T Call<T>() where T : class;
        public abstract Task<T> CallAsync<T>() where T : class;

        public void Publish<T>(T message, string routeKey, Action<IBasicProperties> configureProperties)
            where T : class
        {
            if (message == null)
                throw new RabbitEmptyMessageException("Message is null");

            var channel = _objectPool.Get();

            try
            {
                if (string.IsNullOrEmpty(_exchangeType) || string.IsNullOrEmpty(_exchnageName))
                    throw new RabbitEmptyExchangeSettingsException("You didn't set exchange settings. Before you call Publish() or Call(), you should set exchange settings by SetExchnage() method");

                channel.ExchangeDeclare(_exchnageName, _exchangeType, _isDurable, _autoDelete);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                
                var props = channel.CreateBasicProperties();
                configureProperties.Invoke(props);

                channel.BasicPublish(_exchnageName, routeKey, props, sendBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }

        public void SetExchange(string exchangeName, string exchangeType, bool isDurable, bool autoDelete) =>
            (_exchnageName, _exchangeType, _isDurable, _autoDelete) = (_exchnageName, exchangeType, isDurable, autoDelete);
    }
}
