using FinalProject.Services.Models;
using FinalProject.Services.Plane_service;
using FinalProject.Services.Repositries;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Hubs
{
    public class MainHub : Hub, IMainHub
    {
        static IAirport airport;
        public MainHub(IAirport air)
        {
            airport = air;
        }
        public async Task StaionChanged(Station[] airport)
        {
            try
            {
                await Clients.All.SendAsync("AirPortChanged", airport);
            }
            catch (Exception err)
            {
                Debug.WriteLine($"in eror its :  {err.Message}");
            }
        }
        public async Task RequstChanged(List<Requst> requsts)
        {
            try
            {
                await Clients.All.SendAsync("RequstListUpdate", requsts);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err);
            }
        }

        public async Task ReciveRequst(Requst req)
        {
            await airport.AddRequst(req);
        }
    }
}
