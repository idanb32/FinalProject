using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Connections
{
    public class ServerToClient
    {
        static HubConnection connection;
        public HubConnection Current { get { return connection; } }
        public ServerToClient()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder().WithUrl("http://localhost:4208/MainHub").Build();
            }
            connection.StartAsync();
        }
    }
}
