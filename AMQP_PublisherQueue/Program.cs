﻿using System;
using System.Text;
using RabbitMQ.Client;

namespace AMQP_PublisherQueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //declare connection
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            //use connection
            using (var connection = factory.CreateConnection())
            {
                //create channel
                using (var channel = connection.CreateModel())
                {
                    //declare-create a queue
                    channel.QueueDeclare(   queue:"queueTest",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
                    //create a message
                    string message = "Hello World";
                    var body = Encoding.UTF8.GetBytes(message);

                    //publish the message 
                    channel.BasicPublish(   exchange: "",
                                            routingKey: "queueTest",
                                            mandatory: true,
                                            basicProperties: null,
                                            body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine(); 
        }
    }
}
