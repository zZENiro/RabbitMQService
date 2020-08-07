using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQServiceApp.Exceptions
{
    public class RabbitEmptyMessageException : ApplicationException
    {
        public RabbitEmptyMessageException(string message) : base(message)
        {
        }
    }
}
