using System;
using System.IO;
using System.Security.Cryptography;
using ModernMessageBoxLib;
namespace MeiPassword.BackgroundSysteme
{
    public class FileObfusicator
    {
        private static byte saltss = Convert.ToByte(Algorythmen.Auth_Class_System.salt_key);
       
        public static void Crypto(string inputFile)
        {
            byte[] salt = new byte[saltss];
            FileStream fsCrypt = new FileStream(inputFile + Algorythmen.PathFinding.filename, FileMode.Create);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(Algorythmen.Auth_Class_System.password_crypt);
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

        public static void Decrypter(string inputFile, string outputFile)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(Algorythmen.Auth_Class_System.password_crypt);
            byte[] salt = new byte[saltss];
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

    }
}