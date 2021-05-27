using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MeiPassword.Algorythmen;
using MeiPassword.BackgroundSysteme;
using MeiPassword.ConfigsSystem;
using ModernMessageBoxLib;

namespace MeiPassword.UI_Management
{
    /// <summary>
    /// Interaktionslogik für OutputData.xaml
    /// </summary>
    public partial class OutputData : Window
    {

        public OutputData()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            btnStart_Click_1();
            rename();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSpace.CryptUsername = "";
            SaveSpace.CryptPW = "";
            Clipboard.Clear();
            this.Close();
        }

        private void Copy_EmailPass_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();
            string username = Encoding.UTF8.GetString(Convert.FromBase64String(SaveSpace.CryptUsername));
            Clipboard.SetText(username);
            username = "";
            var msg = new ModernMessageBox("Deine Email/Username befindet sich in deiner Zwischenablage", "Application Info", ModernMessageboxIcons.Info, "Ok")
            {
                Button1Key = Key.D1,
            };
            msg.Show();
        }

        private void Copy_Pass_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();

            string password = Encoding.UTF8.GetString(Convert.FromBase64String(SaveSpace.CryptPW));
            Clipboard.SetText(password);
            password = "";
            
             var msg = new ModernMessageBox("Dein Passwort befindet sich in deiner Zwischenablage", "Application Info", ModernMessageboxIcons.Info, "Ok")
             {
                 Button1Key = Key.D1,
             };
             msg.Show();
        }

        private System.Windows.Forms.Timer timer1;
        private int counter = 60;

        private void btnStart_Click_1()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000;
            timer1.Start();
            timer_programm.Content = counter.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                timer1.Stop();
                Clipboard.Clear();
                SaveSpace.CryptUsername = "";
                SaveSpace.CryptPW = "";
                this.Close();
            }
                
            timer_programm.Content = counter.ToString();
        }



        public void rename()
        {
            int data = Check_Start.checkvaleu();
            if (data == 1) englisch();
            if (data == 0) german();
        }

        void englisch()
        {
            timertitle.Content = "Time remaining";
        }

        void german()
        {
            timertitle.Content = "Zeit verbleibend";
        }
    }
}
