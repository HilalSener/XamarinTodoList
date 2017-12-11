using System;
using TestProject.Droid;
using Xamarin.Forms;
using TestProject.Interfaces;
using System.IO;
using System.Collections.Generic;

[assembly: Dependency(typeof(FileHelper))]
namespace TestProject.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}