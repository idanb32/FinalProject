using FinalProject.Services.Models;
using FinalProject.Services.Plane_service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services.Repositries
{
    public interface IRepository
    {
        public Task AddHistory(string HistoryLine);
        public List<string> GetHistory();
        public Task AddRequst(Requst req);
        public List<Requst> GetRequsts();
        public Task RemoveRequst(Requst req);
    }
}
