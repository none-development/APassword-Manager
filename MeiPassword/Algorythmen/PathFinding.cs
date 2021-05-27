using MeiPassword.ConfigsSystem;
using System;
using System.IO;

namespace MeiPassword.Algorythmen
{
    public class PathFinding
    {
        public static string LOCALROW = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string CURRENPATH = Environment.CurrentDirectory;
        public static string LACALFOLDERConfig = Path.Combine(LOCALROW, @"APWA\config\");
        public static string PASSWORDFOLDER = Path.Combine(LOCALROW, @"APWA\PSWORD\");
        public static string MAIN = Path.Combine(LOCALROW, @"APWA\");
        public static string CONFIGFILE = AppDomain.CurrentDomain.BaseDirectory + @"data\config.ini";
        public static string filename = ".apwm";
        public static string filename_message = ".smapws";
        public static string messSyst = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string MessageSpace = Path.Combine(messSyst + @"\APWA\");



        public static string Pathpasswords()
        {
            var MyIni = new IniFile(CONFIGFILE);
            string b = MyIni.Read("Lang", "System");
            string hst = "";
            if (b == "")
            {
               hst = Path.Combine(LOCALROW, @"APWA\PSWORD\");
            }

            if(b != "")
            {
                hst = b;
            }
            return hst;
        }

    }

}