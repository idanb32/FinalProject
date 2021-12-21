using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class Connection
    {
        static HubConnection connection;
        public HubConnection Current { get { return connection; } }
        public Connection()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder().WithUrl("http://localhost:4208/MainHub").Build();
            }
            connection.StartAsync();
        }
    }
}
