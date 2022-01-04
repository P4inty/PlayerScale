using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Server
{
    public class Server : BaseScript
    {
        public Server()
        {
           EventHandlers.Add("server:scale", new Action<int, double>(Scale));
        }

        private void Scale(int source, double value)
        {
            Debug.WriteLine(Players.ElementAt(source).Name + " tried to scale to: " + value);
            if (value < 0.5 || value > 1.0)
            {
                TriggerClientEvent(Players.ElementAt(source), "client:scale:invalid");
                return;
            }
            TriggerClientEvent("client:scale:valid", value);
        }
    }
}
