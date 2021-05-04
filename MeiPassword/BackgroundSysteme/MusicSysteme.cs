using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeiPassword.BackgroundSysteme
{
    public static class MusicSysteme
    {
        public static bool controlle = false;
        public static void play(bool b)
        {
            if (b)
            {
                musicselector();
                controlle = true;
            } 
            {
                CheckerBoolean(false, false, false, true);
                controlle = false;
            }
        }


        private static void musicselector()
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            string def = MyIni.Read("Track_default", "TrackToRun");
            string tr1 = MyIni.Read("Track_2", "TrackToRun");
            string tr2 = MyIni.Read("Track_3", "TrackToRun");
            bool _default = Boolean.Parse(def);
            bool _tr1 = Boolean.Parse(tr1);
            bool _tr2 = Boolean.Parse(tr2);
            CheckerBoolean(_default, _tr1, _tr2, false);

        }


        public static void CheckerBoolean(bool defau, bool tr1, bool tr2, bool end)
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            Thread a = new Thread(_default_);
            Thread b = new Thread(_tr2s_);
            Thread c = new Thread(_tr1_);
            if (defau) a.Start();
            if (tr1) b.Start();
            if (tr2) c.Start();
            if (end)
            {
                try
                {
                    a.Abort();
                    b.Abort();
                    c.Abort();
                } catch
                { }
            }
        }


        public static void _tr1_()
        {
              SoundPlayer sound = new SoundPlayer(MeiPassword.Audios.audiowav);
              sound.PlayLooping();
        }
        public static void _tr2s_()
        {
            SoundPlayer sound = new SoundPlayer(MeiPassword.Audios.audiowav);
            sound.PlayLooping();
        }
        public static void _default_()
        {
            SoundPlayer sound = new SoundPlayer(MeiPassword.Audios.test2h);
            sound.PlayLooping();
        }

    }
}
