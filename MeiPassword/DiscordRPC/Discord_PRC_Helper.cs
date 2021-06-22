using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeiPassword.DiscordRPC
{
    static class Discord_PRC_Helper
    {
        public static string rcpid = "838389755146010645";

        public static string sik = "eye";

        public static string thurl = "https://simp-to.me/";

        public static string Limgk()
        {
            return images.ToList().PickRandom();
        }
        public static string Limgt()
        {
            return largedescription.ToList().PickRandom();
        }

        private static string[] images = { "1144911", "5565", "abbeb", "asdasda", "asddda", "cacgvee", "dada", "eddof", "main", "safasfas", "vdvs" };
        private static string[] largedescription = { "Using APassword Manager", "https://simp-to.me/", "Made by NONE & Matome", "Weeby Discord RPC", ">.<" };
        private static T PickRandom<T>(this List<T> enumerable)
        {
            int index = new System.Random().Next(0, enumerable.Count());
            return enumerable[index];

        }
    }
}
