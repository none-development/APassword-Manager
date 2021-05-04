using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeiPassword.Algorythmen;

namespace MeiPassword.ConfigsSystem
{
    class Check_Start
    {

        public static void Check()
        {
            if (!Directory.Exists(PathFinding.MAIN))
            {
                Directory.CreateDirectory(PathFinding.MAIN);
            }

            if (!Directory.Exists(PathFinding.PASSWORDFOLDER))
            {
                Directory.CreateDirectory(PathFinding.PASSWORDFOLDER);
            }
            if (!Directory.Exists(PathFinding.PASSWORDFOLDER))
            {
                Directory.CreateDirectory(PathFinding.PASSWORDFOLDER);
            }
            if (!Directory.Exists(PathFinding.MessageSpace))
            {
                Directory.CreateDirectory(PathFinding.MessageSpace);
            }
            if (!File.Exists(PathFinding.CONFIGFILE))
            {
                var MyIni = new IniFile(PathFinding.CONFIGFILE);
                MyIni.Write("Music", "false", "Audio");

                //General Infos
                MyIni.Write("PrivatePolicy", "false", "System");
                MyIni.Write("DiscordRPC", "true", "System");
                MyIni.Write("AutoLogin", "false", "System");

                //Save Passwords
                MyIni.Write("PIN", "", "PasswortFileSystem");
                MyIni.Write("PSW1", "", "PasswortFileSystem");
                MyIni.Write("PSW2", "", "PasswortFileSystem");

                //Colors
                MyIni.Write("Maincolor", "FF424242", "ProgrammFarbSchema");
                MyIni.Write("RGB_Mode", "false", "ProgrammFarbSchema");
                MyIni.Write("WhiteMode", "false", "ProgrammFarbSchema");

                //Audio Tracks
                MyIni.Write("Track_default", "true", "TrackToRun");
                MyIni.Write("Track_2", "false", "TrackToRun");
                MyIni.Write("Track_3", "false", "TrackToRun");
            }


        }
    }
}
