using System.Text;

namespace JustAnotherUnityProject.Common.Scripts.StaticExtensions
{
    internal static class XORExtensions
    {
        internal static string EncryptDecryptXOR(this string str, int cryptoKey)
        {
            StringBuilder stringBuilder = new StringBuilder(str.Length);

            foreach (char ch in str)
            {
                stringBuilder.Append(ch ^ cryptoKey);
            }

            return stringBuilder.ToString();
        }

        internal static void EncryptDecryptXOR(this byte[] array, int cryptoKey)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)(array[i] ^ cryptoKey);
            }
        }
    }
}