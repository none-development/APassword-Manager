using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeiPassword.DiscordRPC
{
    class Discord_Stages
    {
        public static bool Discord;
        public static readonly DiscordRpc.RichPresence Presence = new DiscordRpc.RichPresence();
        public static void login()
        {
            
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(Discord_PRC_Helper.rcpid, ref handlers, false, string.Empty);
            Presence.state = Discord_PRC_Helper.Limgt();
            Presence.details = "Login Screen";
            Presence.startTimestamp = default(long);
            Presence.largeImageKey = Discord_PRC_Helper.Limgk();
            Presence.largeImageText = Discord_PRC_Helper.Limgt();
            Presence.smallImageKey = Discord_PRC_Helper.sik;
            Presence.smallImageText = Discord_PRC_Helper.thurl;

            DiscordRpc.UpdatePresence(Presence);
        }
        public static void MainScreen()
        {
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(Discord_PRC_Helper.rcpid, ref handlers, false, string.Empty);
            Presence.state = Discord_PRC_Helper.Limgt();
            Presence.details = "In Main Screen";
            Presence.startTimestamp = default(long);
            Presence.largeImageKey = Discord_PRC_Helper.Limgk();
            Presence.largeImageText = Discord_PRC_Helper.Limgt();
            Presence.smallImageKey = Discord_PRC_Helper.sik;
            Presence.smallImageText = Discord_PRC_Helper.thurl;
            DiscordRpc.UpdatePresence(Presence);

        }
        public static void Settings()
        {
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(Discord_PRC_Helper.rcpid, ref handlers, false, string.Empty);
            Presence.state = Discord_PRC_Helper.Limgt();
            Presence.details = "In Settings";
            Presence.startTimestamp = default(long);
            Presence.largeImageKey = Discord_PRC_Helper.Limgk();
            Presence.largeImageText = Discord_PRC_Helper.Limgt();
            Presence.smallImageKey = Discord_PRC_Helper.sik;
            Presence.smallImageText = Discord_PRC_Helper.thurl;
            DiscordRpc.UpdatePresence(Presence);
        }
    }
}
