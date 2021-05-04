using System;
using System.IO;
using System.Security.Cryptography;
using ModernMessageBoxLib;
using ModernWpf;
namespace MeiPassword.BackgroundSysteme
{
    public class FileObfusicator
    {
        private static int salt = Int32.Parse(Algorythmen.Auth_Class_System.salt_key);

        private static void encryptStuff(string sDir, string Filename)
        {
            string f = Path.Combine(sDir + Filename);
            try
            {
                if (!f.Contains(Algorythmen.PathFinding.filename))
                {
                    Crypto(f, Algorythmen.Auth_Class_System.password_crypt);
                    File.Delete(f);
                }
            }
            catch (Exception e)
            {
                QModernMessageBox.Show($"Error Message:\n{e}", "Error Appeard!", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);
            }
        }

        private static void Crypto(string inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();
            FileStream fsCrypt = new FileStream(inputFile + Algorythmen.PathFinding.filename, FileMode.Create);
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



        public static byte[] GenerateRandomSalt()
        {
           
            byte[] data = new byte[salt];

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