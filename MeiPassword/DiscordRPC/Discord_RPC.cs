using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;
using Newtonsoft.Json;

namespace MeiPassword.DiscordRPC
{
    public class Discord_RPC
    {
        public DiscordRpcClient client;
      
        static bool b { get; set; }




        public void start()
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            string Discord = MyIni.Read("System", "DiscordRPC").ToString();
            if (Discord.Contains("true") && Discord.Contains("false"))
            {
                b = Boolean.Parse(Discord);
            }
            else
            {
                b = true;
            }

            if (b)
            {
                rpc();
            }
            else
            {
                
            }

        }

        public void rpc()
        {
            client = new DiscordRpcClient("838389755146010645");
            client.Initialize();
                client.ClearPresence();
                client.SetPresence(new RichPresence()
                {
                    Details = $"Using APM",
                    State = "Mainscreen",
                    Assets = new Assets()
                    {
                        LargeImageKey = "main",
                        LargeImageText = "Azusa Passwort Manager",
                        SmallImageKey = "1144911"
                    },
                    Buttons = new Button[]
                        {
                            new Button() { Label = "Get APM", Url = "https://github.com/Azusa-chxn/APassword-Manager" },
                            new Button() { Label = "Some Music", Url = "https://youtu.be/J8GMOKcpcj4" }
                        }
                });
        }
    }
}
