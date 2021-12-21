using FinalProject.Services.Models;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace FinalProject.Services.Plane_service
{

    public class StationService
    {
        private SemaphoreSlim somaphore;

        private Func<string, Task> log;
        private int delay = 1500;

        public int StationNum { get; set; }
        public Plane[] Plane { get; set; }

        public StationService(int staionNum, Func<string, Task> log)
        {
            this.log = log;
            this.StationNum = staionNum;
            if (this.StationNum == 67)
            {
                Plane = new Plane[2];
                somaphore = new SemaphoreSlim(2);
            }
            else
            {
                Plane = new Plane[1];
                somaphore = new SemaphoreSlim(1);
            }

        }

        public async Task MovePlaneToHere(Plane plane)
        {
            if (plane != null)
            {
                await somaphore.WaitAsync();
                if (StationNum != 67)
                {
                    await Task.Delay(delay);
                    this.Plane[0] = plane;
                    await log($"Plane: {plane.name} got station number {StationNum} at {DateTime.Now}");
                }
                else
                {
                    if (Plane[0] == null)
                    {
                        this.Plane[0] = plane;
                        await Task.Delay(delay);
                        await log($"Plane: {plane.name} got station number 6 at {DateTime.Now}");
                    }
                    else
                    {
                        this.Plane[1] = plane;
                        await Task.Delay(delay);
                        await log($"Plane: {plane.name} got station number 7 at {DateTime.Now}");
                    }
                }
            }
        }
        public async Task RemovePlane(Plane removeMe = null)
        {
            if (Plane != null)
            {
                if (StationNum != 67)
                {
                    await log($"Plane: {Plane[0].name} left station number {StationNum} at {DateTime.Now}");
                    this.Plane[0] = null;
                }
                else
                {
                    if (Plane[0] == removeMe && removeMe != null)
                    {
                        await log($"Plane: {Plane[0].name} left station number {6} at {DateTime.Now}");
                        this.Plane[0] = null;
                    }
                    else
                    {
                        await log($"Plane: {Plane[1].name} left station number {7} at {DateTime.Now}");
                        this.Plane[1] = null;
                    }
                }
                somaphore.Release();
            }
        }
    }
}
