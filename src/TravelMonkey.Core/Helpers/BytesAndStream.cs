using System;
using System.IO;
using System.Threading.Tasks;

namespace TravelMonkey.Core.Helpers
{
    public static class BytesAndStream
    {
        public static async Task<byte[]> ToByteArray(this Stream input)
        {
            byte[] bytes;
            bytes = new byte[input.Length];
            await input.ReadAsync(bytes, 0, (int)input.Length);
            return bytes;
        }

        public static async Task<Stream> ToStream(this byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            return stream;
        }

        public static string ToHTMLString(this byte[]bytes)
        {
            return $"data:image/jpg;base64,{Convert.ToBase64String(bytes)}";
        }

    }
}
