using FinalProject.Services.Plane_service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services.Models
{
    public class Requst
    {
        public int RequstId{ get; set; }
        public int IncPlaneName { get; set; }
        public RequestType WhatRequsted { get; set; }
    }
}
