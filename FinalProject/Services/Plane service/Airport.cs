using FinalProject.Connections;
using FinalProject.Hubs;
using FinalProject.Services.Models;
using FinalProject.Services.Repositries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Services.Plane_service
{
    public enum RequestType
    {
        Departure,
        Arrivale
    }
    public class Airport : IAirport
    {
        private readonly IRepository myRepo;
        private readonly ServerToClient connection;
        private List<Requst> requsts;

        public ConcurrentQueue<Requst> UnsolvedRequsts { get; set; }
        public Station[] myAirPortModel;
        public StationService[] myAirPortServices;
        public StationService[] DepartureRoute;
        public StationService[] ArrivaleRoute;

        public Airport(IRepository myRepo)
        {
            Debug.WriteLine("got into constructor");
            this.myRepo = myRepo;
            myAirPortModel = new Station[8];
            myAirPortServices = new StationService[7];
            connection = new ServerToClient();
            makeNewAirPort();
            requsts = this.myRepo.GetRequsts();
            UnsolvedRequsts = new ConcurrentQueue<Requst>(requsts);
            SetupArrivaleRoutes();
            SetupDepartureRoutes();
            HandleRequst();
        }

        private void SetupDepartureRoutes()
        {
            DepartureRoute = new StationService[3];
            DepartureRoute[0] = myAirPortServices[5];
            DepartureRoute[1] = myAirPortServices[6];
            DepartureRoute[2] = myAirPortServices[3];
        }

        private void SetupArrivaleRoutes()
        {
            ArrivaleRoute = new StationService[6];
            for (int i = 0; i < 6; i++)
            {
                ArrivaleRoute[i] = myAirPortServices[i];
            }

        }
     
        private async Task updateClient()
        {
            for (int i = 0; i < 5; i++)
            {
                myAirPortModel[i].plane = myAirPortServices[i].Plane[0];
            }
            myAirPortModel[5].plane = myAirPortServices[5].Plane[0];
            myAirPortModel[6].plane = myAirPortServices[5].Plane[1];
            myAirPortModel[7].plane = myAirPortServices[6].Plane[0];

            if (myAirPortModel != null && canISendToClient())
            {
                await connection.Current.InvokeAsync("StaionChanged", myAirPortModel);
            }
        }
        private bool canISendToClient()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int y = i + 1; y < 7; y++)
                {
                    if (myAirPortModel[i].plane != null && myAirPortModel[y].plane != null)
                        if (myAirPortModel[i].plane.name == myAirPortModel[y].plane.name)
                            return false;
                }
            }
            return true;
        }

        private void makeNewAirPort()
        {

            for (int i = 1; i <= 5; i++)
            {
                myAirPortModel[i - 1] = new Station { stationNum = i };
                myAirPortServices[i - 1] = new StationService(i, myRepo.AddHistory);
            }
            myAirPortModel[5] = new Station { stationNum = 6 };
            myAirPortModel[6] = new Station { stationNum = 7 };
            myAirPortServices[5] = new StationService(67, myRepo.AddHistory);
            myAirPortModel[7] = new Station { stationNum = 8 };
            myAirPortServices[6] = new StationService(8, myRepo.AddHistory);
        }

        public async Task AddRequst(Requst req)
        {
            UnsolvedRequsts.Enqueue(req);
            requsts.Add(req);
            await myRepo.AddRequst(req);
            await connection.Current.InvokeAsync("RequstChanged", requsts);
        }
        private async Task updateRequst(Requst req)
        {
            requsts.Remove(req);
            await myRepo.RemoveRequst(req);
            await connection.Current.InvokeAsync("RequstChanged", requsts);
        }

        private void HandleRequst()
        {
            Task.Run(async () =>
           {
               while (true)
                   if (UnsolvedRequsts.TryDequeue(out var req))
                   {
                       if (req.WhatRequsted == RequestType.Arrivale)
                       {
                           var landing = new LandingOrTakeOff(ArrivaleRoute, updateClient, updateRequst, req);
                           landing.Land();
                       }
                       
                       else
                       {
                           var landing = new LandingOrTakeOff(DepartureRoute, updateClient, updateRequst, req);
                           landing.Land();
                       }
                     
                   }
                   else
                       Thread.Sleep(1000);
           });
        }

    }
}
