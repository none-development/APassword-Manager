using System;
using System.IO;

namespace MeiPassword.Algorythmen
{
    public class PathFinding
    {
        public static string LOCALROW = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string LACALFOLDERConfig = Path.Combine(LOCALROW, @"APWA\config\");
        public static string PASSWORDFOLDER = Path.Combine(LOCALROW, @"APWA\PSWORD\");
        public static string MAIN = Path.Combine(LOCALROW, @"APWA\");
        public static string CONFIGFILE = LOCALROW + @"\APWA\config.ini";
        public static string filename = ".apwm";
        public static string filename_message = ".smapws";
        public static string messSyst = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string MessageSpace = Path.Combine(messSyst + @"\APWA\");



        public static check() { }
    }
}