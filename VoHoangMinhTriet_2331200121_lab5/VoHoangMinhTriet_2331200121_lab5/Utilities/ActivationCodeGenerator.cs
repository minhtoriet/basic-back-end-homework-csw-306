using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace VoHoangMinhTriet_2331200121_lab5.Utilities
{
    public class ActivationCodeGenerator
    {
        private static readonly char[] Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        public static string GenerateCode()
        {
            StringBuilder result = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                int idx = RandomNumberGenerator.GetInt32(Characters.Length);
                result.Append(Characters[idx]);
            }
            return result.ToString();
        }
    }
}
