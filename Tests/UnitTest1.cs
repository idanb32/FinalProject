using FinalProject.Hubs;
using FinalProject.Services.Models;
using FinalProject.Services.Plane_service;
using FinalProject.Services.Repositries;
using FinalProject.Services.UnitTestInterface;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        MainHub hub;
        ITestAirport testAirPort;
        Object locker = new object();
        public UnitTest1()
        {
            //var options = new DbContextOptionsBuilder<PlaneContext>().UseInMemoryDatabase(databaseName: "DbContextDatabase").Options;
            //var context = new PlaneContext(options);
            //var rep = new PlaneRep(context);
            //var testAirPort = new Airport(rep);
            //this.testAirPort = testAirPort;
            //hub = new MainHub(testAirPort);
        }
      
        // new fly got into station

        // new departure got into station

        // fly moved to next station

        // fly got out of station when finished course

        [TestMethod]
        public async void TestMethod1()
        {
            testAirPort.TestPlaneAddedToAirport += TestMe;
            Monitor.Enter(locker);
            Monitor.Wait(locker);
            testAirPort.TestPlaneAddedToAirport -= TestMe;
            
            Assert.IsTrue(true);

        }

        private void TestMe(object sender, EventArgs e)
        {

            Monitor.Exit(locker);
        }
    }
}
