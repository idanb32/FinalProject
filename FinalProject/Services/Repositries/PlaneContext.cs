using FinalProject.Services.Models;
using FinalProject.Services.Plane_service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services.Repositries
{
    public class PlaneContext : DbContext
    {
        public PlaneContext(DbContextOptions<PlaneContext> options) : base(options)
        {
        }
        public DbSet<History> History { get; set; }
        public DbSet<Requst> UnsolvedReq { get; set; }
        //public Station[] MyAirport { get; set; }
    }
}
