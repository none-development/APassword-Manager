using System.Windows;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ModernMessageBoxLib;
using MeisXOR;
using System.IO;
using System.Security.Cryptography;
using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;
using MeiPassword.BackgroundSysteme;

namespace MeiPassword.UI_Management
{
    /// <summary>
    /// Interaktionslogik für ControlScreen.xaml
    /// </summary>
    public partial class ControlScreen : Window
    {
      
        public ControlScreen()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            start(); listadd();
            DiscordRPC.Discord_RPC.rpc(false, false, true, false, false);
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.Discord_RPC.rpc(false, false, false, true, false);
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
            var msg = new ModernMessageBox("Dein Generiertes Passwort befindet sich in deiner Zwischenablage", "Application Info", ModernMessageboxIcons.Info, "Ok")
            {
                Button1Key = Key.D1,
            };
            msg.Show();

        }

        private string RandomString(int length, bool chinese) 
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_{+-%#";
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
            SecureFile scf = new SecureFile();
            scf.Show();
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

        private void SaveNewPass_Click(object sender, RoutedEventArgs e)
        {
            bool b = false;
            if(email_save.Text != "" && password_save.Password != ""  && FileName.Text != "")
            {
                string email = Convert.ToBase64String(Encoding.UTF8.GetBytes(email_save.Text));
                string passwort_save = Convert.ToBase64String(Encoding.UTF8.GetBytes(password_save.Password));
                string filename = FileName.Text;
                foreach(string file in files())
                {
                    if(file == $"{filename}.apwm")
                    {
                        b = true;
                    }
                }

                if (b)
                {
                    QModernMessageBox.Show($"Error:\nPassword {filename} already exist", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);
                    return;
                }

                if (!b)
                {
                    var MyIni = new IniFile(PathFinding.PASSWORDFOLDER + $"{filename}.ini");
                    MyIni.Write("USERNAMEEMAIL", email, "DATA");
                    MyIni.Write("PASSWORDPASS", passwort_save, "DATA");
                    FileObfusicator.Crypto(PathFinding.PASSWORDFOLDER + $"{filename}.ini");
                    File.Move(PathFinding.PASSWORDFOLDER + $"{filename}.ini.apwm", PathFinding.PASSWORDFOLDER + $"{filename}.apwm");
                    File.Delete(PathFinding.PASSWORDFOLDER + $"{filename}.ini.apwm");
                    QModernMessageBox.Show($"Dein Passwort wurde gespeichert", "Erfolg!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
                    cleatlist();
                    listadd();
                    return;
                }

            }
            QModernMessageBox.Show($"Du kannst nicht Nichts als Passwort eintragen", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);

        }

        public void listadd()
        {
            foreach(string a in files())
            {
                string b = Path.GetFileNameWithoutExtension(a);
                Passwords.Items.Add(b);
            }
        }

        public void cleatlist()
        {
            Passwords.Items.Clear();
        }

        public static string[] files()
        {
            string[] b = Directory.GetFiles(Algorythmen.PathFinding.PASSWORDFOLDER);
            return b;
        }

        private void EntschluesselSelected_Click(object sender, RoutedEventArgs e)
        {
            string name = Passwords.SelectedItem.ToString() + ".apwm";
            string path = Path.Combine(PathFinding.PASSWORDFOLDER, name);
            string newfilenametemp = RandomString2(12);
            string outpath = Path.Combine(PathFinding.PASSWORDFOLDER, newfilenametemp + ".ini");
            FileObfusicator.Decrypter(path, outpath);
            var MyIni = new IniFile(outpath);
            SaveSpace.CryptUsername = MyIni.Read("USERNAMEEMAIL", "DATA");
            SaveSpace.CryptPW = MyIni.Read("PASSWORDPASS", "DATA");
            File.Delete(outpath);
            OutputData b = new OutputData();
            b.Show();

        }

        private void deleteselected_Click(object sender, RoutedEventArgs e)
        {
            string name =  Passwords.SelectedItem.ToString() + ".apwm";
            File.Delete(Path.Combine(PathFinding.PASSWORDFOLDER, name));
            cleatlist();
            listadd();
            QModernMessageBox.Show($"Das Passwort wurde gelöscht", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);
        }


    }
}