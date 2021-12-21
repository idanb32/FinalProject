using FinalProject.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Services.Plane_service
{
    public class RoutingService
    {
        private int position;
        StationService[] route;
        RequestType reqType;
        Plane incPlane;

        public RoutingService(StationService[] route, Requst req)
        {
            position = 0;
            this.route = route;
            this.reqType = req.WhatRequsted;
            this.incPlane = new Plane { name = req.IncPlaneName };
        }
        public async Task<bool> MoveToNextStation()
        {
            if (reqType == RequestType.Arrivale)
            {
                bool resault;
                if (position == 6)
                {
                    route[position - 1].RemovePlane(incPlane).Wait();
                    resault = false;
                }
                else resault = await route[position].MovePlaneToHere(incPlane).ContinueWith((res) =>
                  {
                      if (position > 0)
                          route[position - 1].RemovePlane().Wait();
                      position++;
                      return true;
                  });
                return resault;
            }

            else
            {
                bool resault;
                if (position == 3)
                {
                    route[position - 1].RemovePlane().Wait();
                    resault = false;
                }
                else resault = await route[position].MovePlaneToHere(incPlane).ContinueWith((res1) =>
                 {
                     if (position == 1)
                         route[position - 1].RemovePlane(incPlane).Wait();
                     else if (position == 2)
                         route[position - 1].RemovePlane().Wait();
                     position++;
                     return true;
                 });
                return resault;
            }
        }
    }
}
