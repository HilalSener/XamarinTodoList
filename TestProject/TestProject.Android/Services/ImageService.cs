using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestProject.Interfaces;
using TestProject.Droid.Services;
using Android.Graphics;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(ImageService))]
namespace TestProject.Droid.Services
{
    public class ImageService : IImageService
    {
        public byte[] ResizeImage(byte[] imageData, float maxWidth, float maxHeight)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            var ratioX = (double)maxWidth / originalImage.Width;
            var ratioY = (double)maxHeight / originalImage.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(originalImage.Width * ratio);
            var newHeight = (int)(originalImage.Height * ratio);

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 80, ms);
                return ms.ToArray();
            }
        }
    }
}