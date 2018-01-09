using System;
using PCLStorage;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestProject.Utility
{
    public static class Extension
    {
        public static byte[] ReadFully(this System.IO.Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public async static Task<string> FileToBase64(this string path)
        {
            var file = await FileSystem.Current.GetFileFromPathAsync(path);

            if (file == null) return string.Empty;
            using (Stream stream = await file.OpenAsync(FileAccess.Read))
            {
                if (stream == null) return string.Empty;

                return Convert.ToBase64String(stream.ReadFully());
            }
        }

        public async static Task<byte[]> FileToByteArray(this string path)
        {
            var file = await FileSystem.Current.GetFileFromPathAsync(path);

            if (file == null) return null;
            using (Stream stream = await file.OpenAsync(FileAccess.Read))
            {
                if (stream == null) return null;
                return stream.ReadFully();
            }
        }

        public static ImageSource AsImageSource(this byte[] byteArray)
        {
            ImageSource retSource = null;
            if (byteArray != null)
            {
                byte[] imageAsBytes = byteArray;
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retSource;
        }

    }
}
