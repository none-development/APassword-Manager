using System.Windows;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ModernMessageBoxLib;
using MeisXOR;
using System.IO;
using System.Security.Cryptography;

namespace MeiPassword.UI_Management
{
    /// <summary>
    /// Interaktionslogik für ControlScreen.xaml
    /// </summary>
    public partial class ControlScreen : Window
    {
      
        private readonly string pw = "UㄨB8中CQ2机35先o8LQ先-dj7这_H9JRAPycF明说;进中cGS/f9书{C中A+3";
        private readonly int init = 35;
        public ControlScreen()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            start();
            DiscordRPC.Discord_RPC.rpc(false, true, false, false);
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.Discord_RPC.rpc(false, false, false, true);
            System.Environment.Exit(1);
        }

        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        

        private void start(){
            Untertitel.Content = $"Willkommen {Environment.UserName} bei Azusa Passwort Manager";
            var count = ConfigsSystem.StartUp_Count.files().Length;
            line_zwei.Content = $"{Environment.UserName}, du hast {count} Passwörter gespeichert";
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            UI_Management.Settings data = new UI_Management.Settings();
            data.Show();
        }

        private void GeneratePW_Click(object sender, RoutedEventArgs e)
        {
            int zeichen = Int32.Parse(Anzahl_Zeichen.Text);
            string finish = "";
            if (Chinese_check.IsChecked == false)
            {
                finish = RandomString(zeichen, false);
            }
            else if(Chinese_check.IsChecked == true)
            {
                finish = RandomString(zeichen, true);
            }
            Clipboard.SetText(finish);
            var msg = new ModernMessageBox("Dein Generiertes Passwort befindet sich in deiner Zwischenablage :)", "Azusa Passwort Meneger Massege", ModernMessageboxIcons.Info, "Cool owo")
            {
                Button1Key = Key.D1,
            };
            msg.Show();

        }


        private string RandomString(int length, bool chinese) 
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_{+-;./%#";
            const string poolchinese = "ㄓㄨㄥㄨㄣ中文字将制造款世界上最先进的飞机这是份非常简单的说明书abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_{+-;./%#";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                if (chinese)
                {
                    var c = poolchinese[rand.Next(0, poolchinese.Length)];
                    builder.Append(c);
                }
                else
                {
                    var c = pool[rand.Next(0, pool.Length)];
                    builder.Append(c);
                }

            }

            return builder.ToString();
        }

        private string RandomString2(int length)
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                    var c = pool[rand.Next(0, pool.Length)];
                    builder.Append(c);
            }

            return builder.ToString();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void GenerateKeyFile_Click(object sender, RoutedEventArgs e)
        {
            if(password_massege.Text != "")
            {
                string securetext = XORConverter.MeiXOREncrypt(password_massege.Text, pw);
                string filename = RandomString2(12);
                File.AppendAllText(Algorythmen.PathFinding.MessageSpace + $"\\{filename}.txt", securetext);
                string f = Path.Combine(Algorythmen.PathFinding.MessageSpace + $"{filename}.txt");
                Crypto2(f, pw);
                string g = Path.Combine(Algorythmen.PathFinding.MessageSpace + $"\\{filename}.txt.smapws");
                string l = Path.Combine(Algorythmen.PathFinding.MessageSpace + $"\\{filename}.smapws");
                File.Move(g, l);
                File.Delete(g);
                QModernMessageBox.Show("Eine Verschlüsselte Datei wurde erstellt", "Application info", QModernMessageBox.QModernMessageBoxButtons.Ok);

            } else
            {
                QModernMessageBox.Show("Bitte verwende ein Passwort welches in die Datei soll", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok);
            }
        }

        public static void Crypto2(string inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt2();
            FileStream fsCrypt = new FileStream(inputFile + Algorythmen.PathFinding.filename_message, FileMode.Create);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CBC;
            fsCrypt.Write(salt, 0, salt.Length);
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cs.Write(buffer, 0, read);
                }

                fsIn.Close();
            }
            catch
            {
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
                File.Delete(inputFile);
            }
        }
        public static byte[] GenerateRandomSalt2()
        {

            byte[] data = new byte[117];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }

            return data;
        }
    }
}