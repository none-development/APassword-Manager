using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using ModernMessageBoxLib;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace MeiPassword.UI_Management
{
    /// <summary>
    /// Interaktionslogik für SecureFile.xaml
    /// </summary>
    public partial class SecureFile : Window
    {
        string password = Encoding.UTF8.GetString(Convert.FromBase64String("SEpjdnpyYnZjYnNkZmdmVXY3c2R2anNlZmdHQ3NjYnY="));
        string path_main { get; set; }
        string path_out { get; set; }
        string entsch_path { get; set; }

        public SecureFile()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            DiscordRPC.Discord_RPC.rpc(false, false, false, false, true);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openFileDialog1.Title = "Wähle Datei aus zum Verschlüsseln";
            openFileDialog1.CheckFileExists = true;  
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                pahtfile.Content = openFileDialog1.FileName;
                path_main = openFileDialog1.FileName;
            }
        }

        private void OutPutPath_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            dialog.IsFolderPicker = true;
            dialog.Title = "Wähle Output Folder aus";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputpath.Content = dialog.FileName;
                path_out = dialog.FileName;
            }
        }





        private void Decrypter(string inputFile, string outputFile)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[65];
            FileStream cryptfiles = new FileStream(inputFile, FileMode.Open);
            cryptfiles.Read(salt, 0, salt.Length);
            RijndaelManaged ert = new RijndaelManaged();
            ert.KeySize = 256;
            ert.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            ert.Key = key.GetBytes(ert.KeySize / 8);
            ert.IV = key.GetBytes(ert.BlockSize / 8);
            ert.Padding = PaddingMode.PKCS7;
            ert.Mode = CipherMode.CBC;

            CryptoStream cryptoStream = new CryptoStream(cryptfiles, ert.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fileStreamOutput = new FileStream(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                {

                    fileStreamOutput.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                QModernMessageBox.Show($"CryptographicException error:\n{ex_CryptographicException.Message}", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);

            }
            catch (Exception ex)
            {

                QModernMessageBox.Show($"Error:\n{ex.Message}", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);
            }

            try
            {
                cryptoStream.Close();
            }
            catch (Exception ex)
            {
                QModernMessageBox.Show($"Error by closing CryptoStream::\n{ex.Message}", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);
            }
            finally
            {
                fileStreamOutput.Close();
                cryptfiles.Close();
            }
        }


        public void Crypto2(string inputFile)
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
            }
        }
        public byte[] GenerateRandomSalt2()
        {

            byte[] data = new byte[65];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (filenameout.Text != "") generate();
            if (filenameout.Text == "") QModernMessageBox.Show($"Bitte wähle einen Namen für die Datei", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
        }


        private void generate()
        {
            Crypto2(path_main);
            if(path_main + Algorythmen.PathFinding.filename_message == path_out + Algorythmen.PathFinding.filename_message)
            {
                File.Move(path_main + Algorythmen.PathFinding.filename_message, path_out + filenameout.Text + Algorythmen.PathFinding.filename_message);
            } else
            {
                File.Move(path_main + Algorythmen.PathFinding.filename_message, path_out + filenameout.Text + Algorythmen.PathFinding.filename_message);
                File.Delete(path_main + Algorythmen.PathFinding.filename_message);
            }
            pahtfile.Content = "Nichts ausgewählt";
            path_main = "";
            outputpath.Content = "Nichts ausgewählt";
            path_out = "";
            QModernMessageBox.Show($"Die Datei wurde erstellt", "Application Info", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);

        }

        private void Entschlüsselwahl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openFileDialog1.Title = "Wähle Datei aus zum Verschlüsseln";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "smapws (*.smapws)|*.smapws";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                entschlusselpath.Content = openFileDialog1.FileName;
                entsch_path = openFileDialog1.FileName;
            }
        }

        private void Entschluesseln_Click(object sender, RoutedEventArgs e)
        {
            if(entsch_path != "")
            {
                Decrypter(entsch_path, entsch_path + ".CHANGEMEBACK");
                QModernMessageBox.Show($"Die Datei wurde entschlüsselt", "Application Info", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
                entschlusselpath.Content = "Nichts ausgewählt";
            }
            if(entsch_path == "")
                QModernMessageBox.Show($"Keine Datei ausgewählt", "Application Info", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
        }
    }
}
