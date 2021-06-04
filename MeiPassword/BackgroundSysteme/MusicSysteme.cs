using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;
using ModernMessageBoxLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AudioAPI;

namespace MeiPassword.BackgroundSysteme
{
    public static class MusicSysteme
    {
        public static bool controlle = false;
        public static int initz = 0;
        public static void play(bool b)
        {
            if (b)
            {
                var MyIni = new IniFile(PathFinding.CONFIGFILE);
                string def = MyIni.Read("Track_default", "TrackToRun");
                string tr1 = MyIni.Read("Track_2", "TrackToRun");
                string tr2 = MyIni.Read("Track_3", "TrackToRun");
                bool _default = Boolean.Parse(def);
                bool _tr1 = Boolean.Parse(tr1);
                bool _tr2 = Boolean.Parse(tr2);
                CheckerBoolean(_default, _tr1, _tr2, false);
                controlle = true;
            } 
            if(b == false){
                CheckerBoolean(false, false, false, true);
                controlle = false;
            }
        }


        public static void CheckerBoolean(bool defau, bool tr1, bool tr2, bool end)
        {
          
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            Thread a = new Thread(_default_);
            Thread b = new Thread(_tr2s_);
            Thread c = new Thread(_tr1_);
            if (defau) a.Start(); initz = 1;
            if (tr1) b.Start(); initz = 2;
            if (tr2) c.Start(); initz = 3;
            if (end)
            {
                switch (initz)
                {
                    case 1:
    
                        a.Abort();
                        break;
                    case 2:
                      
                        b.Abort();
                        break;
                    case 3:
                       
                        c.Abort();
                        break;
                    default:
                        QModernMessageBox.Show($"Error End Audio", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
                        break;
                }
            }
        }


        public static void _tr1_()
        {
            SoundPlayer sound = new SoundPlayer(Audios.m3);
            ssound.PlayLooping();
        }
        public static void _tr2s_()
        {
            SoundPlayer sound = new SoundPlayer(Audios.m2);
            sound.PlayLooping();
        }
        public static void _default_()
        {
            SoundPlayer sound = new SoundPlayer(Audios.m1);
            sound.PlayLooping();
        }

        }
    }
