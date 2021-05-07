using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;

namespace MeiPassword.DiscordRPC
{
    public static class Discord_RPC
    {
        public static DiscordRpcClient client;
      
        static bool b { get; set; }
        public static void start()
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
                rpc(true, false, false, false, true);
            }
            else
            {
                
            }

        }

        public static void rpc(bool start, bool setting, bool menu, bool close, bool nothing)
        {
            client = new DiscordRpcClient("838389755146010645");
            client.Initialize();
            if (start)
            {
                client.ClearPresence();
                client.SetPresence(new RichPresence()
                {
                    Details = "Using APM",
                    State = "Mainscreen",
                    Assets = new Assets()
                    {
                        LargeImageKey = "main",
                        LargeImageText = "Azusa Passwort Manager",
                        SmallImageKey = "1144911"
                    },
                    Buttons = new Button[]
                        {
                            new Button() { Label = "Join Discord", Url = "https://discord.gg/4hxd728g6s" },
                            new Button() { Label = "´Somne Music", Url = "https://youtu.be/J8GMOKcpcj4" }
                        }
                });
            }
            if (setting)
            {
                client.ClearPresence();
                client.SetPresence(new RichPresence()
                {
                    Details = "Using APM",
                    State = "Settings",
                    Assets = new Assets()
                    {
                        LargeImageKey = "main",
                        LargeImageText = "Azusa Passwort Manager",
                        SmallImageKey = "1144911"
                    },
                    Buttons = new Button[]
                    {
                       new Button() { Label = "Join Discord", Url = "https://discord.gg/4hxd728g6s" },
                       new Button() { Label = "OwO", Url = "https://youtu.be/J8GMOKcpcj4" }
                    }

                });
            }

            if (menu)
            {
                client.ClearPresence();
                client.SetPresence(new RichPresence()
                {
                    Details = "Using APM",
                    State = "Login",
                    Assets = new Assets()
                    {
                        LargeImageKey = "main",
                        LargeImageText = "Azusa Passwort Manager",
                        SmallImageKey = "1144911"
                    },
                    Buttons = new Button[]
                    {
                       new Button() { Label = "Join Discord", Url = "https://discord.gg/4hxd728g6s" },
                       new Button() { Label = "OwO", Url = "https://youtu.be/J8GMOKcpcj4" }
                    }
                });
            }
        
        }
    }
}
