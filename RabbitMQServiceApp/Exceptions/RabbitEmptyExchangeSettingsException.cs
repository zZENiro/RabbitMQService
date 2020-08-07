using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQServiceApp.Exceptions
{
    class RabbitEmptyExchangeSettingsException : ApplicationException
    {
        public RabbitEmptyExchangeSettingsException(string message) : base(message)
        {
            
        }
    }
}
