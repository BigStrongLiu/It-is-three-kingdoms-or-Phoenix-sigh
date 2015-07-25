using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class MD5Utils 
{
    public static string PasswordToMD5String(string text)
    {
        byte[] md5ByteArray = MD5.Create().ComputeHash(Encoding.Default.GetBytes(text), 0, text.Length);
        string result = BitConverter.ToString(md5ByteArray).Replace("-", "");
        return result;
    }
    public static byte[] BytesToMD5Bytes(byte[] bytes)
    {
        return MD5.Create().ComputeHash(bytes);
    }
    public static byte[] StreamToMD5Bytes(MemoryStream ms)
    {
        byte[] localMD5 = MD5.Create().ComputeHash(ms);
        ms.Close();
        return localMD5;
    }
}
