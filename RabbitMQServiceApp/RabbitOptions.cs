﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQServiceApp
{
    public class RabbitOptions
    {
        public const string OptionsName = "rabbit";

        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }

        public int Port { get; set; } = 5672;

        public string VHost { get; set; } = "/";
    }
}
