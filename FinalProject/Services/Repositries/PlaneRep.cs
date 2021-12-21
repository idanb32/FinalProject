using FinalProject.Services.Models;
using FinalProject.Services.Plane_service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Services.Repositries
{
    public class PlaneRep : IRepository
    {
        private readonly PlaneContext myDb;
        private SemaphoreSlim somaphore = new SemaphoreSlim(1);
        private SemaphoreSlim somaphoreAirport = new SemaphoreSlim(1);

        public PlaneRep(PlaneContext db)
        {
            myDb = db;
        }
        public async Task AddHistory(string HistoryLine)
        {
            await somaphore.WaitAsync();
            History newHistory = new History { HistoryLine = HistoryLine };
            await myDb.History.AddAsync(newHistory);
            await myDb.SaveChangesAsync();
            somaphore.Release();
        }

        public async Task AddRequst(Requst req)
        {
            await somaphore.WaitAsync();
            await myDb.UnsolvedReq.AddAsync(req);
            await myDb.SaveChangesAsync();
            somaphore.Release();
        }

        public List<string> GetHistory()
        {

            return myDb.History.Select(item => item.HistoryLine).ToList();
        }

        public List<Requst> GetRequsts()
        {
            return myDb.UnsolvedReq.ToList();
        }

        public async Task RemoveRequst(Requst req)
        {
            await somaphore.WaitAsync();
            myDb.UnsolvedReq.Remove(req);
            await myDb.SaveChangesAsync();
            somaphore.Release();
        }

    }
}
