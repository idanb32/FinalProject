using FinalProject.Services.Models;
using FinalProject.Services.Plane_service;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Timers;

namespace Simulator
{
    class Program
    {
        private static readonly Connection connection = new Connection();
        static void Main(string[] args)
        {
            Timer time = new Timer();
            time.Interval = 4500;
            time.Elapsed += SendRequst;
            time.Start();
            while (true) { };
        }

        private static void SendRequst(object sender, ElapsedEventArgs e)
        {
            Random rendom= new Random();
            int req = rendom.Next(1, 3);
            RequestType request;
            if (req == 1)
                request = RequestType.Arrivale;
            else
                request = RequestType.Departure;
            int planeId = rendom.Next(1000, 9999);
            Plane newPlane = new Plane {name=planeId };
            Console.WriteLine($"New requst, at {DateTime.Now} the requst is: {request}, plane id: {newPlane.name}. ");
            Requst theRequstItself = new Requst { IncPlaneName = newPlane.name, WhatRequsted = request };
            connection.Current.InvokeAsync("ReciveRequst", theRequstItself);
            //send theRequstItself
        }
    }
}
