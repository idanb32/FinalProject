using FinalProject.Services.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Hubs
{
    public interface IMainHub
    {
        public Task StaionChanged(Station[] airport);
        public Task RequstChanged(List<Requst> requsts);
        public Task ReciveRequst(Requst req);
    }
}
