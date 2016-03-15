﻿using Nancy.Hosting.Self;
using System;

namespace LogicAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }
}
