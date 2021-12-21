using FinalProject.Services.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FinalProject.Services.Plane_service
{
    public interface IAirport
    {
        ConcurrentQueue<Requst> UnsolvedRequsts { get; set; }

        Task AddRequst(Requst req);
    }
}