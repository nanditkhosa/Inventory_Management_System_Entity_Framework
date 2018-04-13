
using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers.Security
{
    /// <summary>
    /// Security Helper
    /// </summary>
    public static class SecurityHelper
    {

        /// <summary>
        /// Returns Hashed password given a plain password
        /// </summary>
        /// <param name="password">plain text password</param>
        /// <param name="salt">password</param>
        /// <returns></returns>
        public static string HashPassword(string password,  string salt)
        {
            if (string.IsNullOrEmpty(salt))
            {
                var saltByte = new byte[16];
                var randomData = RandomNumberGenerator.Create();
                randomData.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);
            }
            var passwordByte = Encoding.Unicode.GetBytes(salt + password);
            var hashedBytes = SHA512.Create().ComputeHash(passwordByte);
            var hashedPassword = Convert.ToBase64String(hashedBytes);
            return hashedPassword;
        }

        /// <summary>
        /// Computes Hash 
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        public static string base64Encode(string sData) // Encode    
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
    }
} // class
 // namespace
