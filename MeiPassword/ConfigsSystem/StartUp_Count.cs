using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeiPassword.ConfigsSystem
{
    public static class StartUp_Count
    {
        public static int Dateien { get; set; }

        public static string[] files()
        {
            string[] b = Directory.GetFiles(Algorythmen.PathFinding.PASSWORDFOLDER, "*.apwm");
            return b;
        }

    }
}
