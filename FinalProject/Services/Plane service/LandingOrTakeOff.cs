using FinalProject.Services.Models;
using System;
using System.Threading.Tasks;

namespace FinalProject.Services.Plane_service
{
    internal class LandingOrTakeOff
    {
        private Func<Task> updateClient;
        private Func<Requst,Task> updateReq;
        private Requst req;
        public RoutingService routingPath { get; set; }
        public Plane routeThisPlane { get; set; }

        public LandingOrTakeOff(StationService[] routeingPath, Func<Task> updateClient, Func<Requst,Task> updateReq, Requst req)
        {
            this.req = req;
            this.routingPath = new RoutingService(routeingPath,req);
            this.updateClient = updateClient;
            this.updateReq = updateReq;
            this.routeThisPlane = new Plane { name= req.IncPlaneName };
        }

        public async Task Land()
        {
            while (await routingPath.MoveToNextStation())
            {
                await updateClient();
            }
           await updateReq(req);
        }
    }
}