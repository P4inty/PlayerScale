using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client
{
    public class Client : BaseScript
    {
        public Client()
        {
            EventHandlers.Add("onClientResourceStart", new Action<string>(OnClientResourceStart));
            EventHandlers.Add("client:scale:invalid", new Action(InvalidScale));
            EventHandlers.Add("client:scale:valid", new Action<double>(ValidScale));
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() == resourceName) return;

            RegisterCommand("scale", new Action<int, List<object>>(Scale), false);
        }

        private void Scale(int source, List<object> args)
        {
            if (!double.TryParse(args[0].ToString(), out var scale))
            {
                Message("Der Skalierungswert muss eine Komma Zahl sein (z.B 0.9).");
                return;
            }
            TriggerServerEvent("server:scale",source, scale);
        }

        private void InvalidScale()
        {
            Message("Der Skalierungswert muss zwischen 0.5 und 1.0 liegen.");
        }

        private void ValidScale(double scale)
        {
            SetPedScale(PlayerPedId(), (float)scale);
        }

        private void Message(string message)
        {
            TriggerEvent("chat:addMessage", new
            {
                color = new[] {255, 0 , 0},
                args = new[] {"[Scaling] " + message}
            });
        }
    }
}
