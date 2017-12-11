using System;
using TestProject.iOS.DependencyServices;
using Xamarin.Forms;
using TestProject.Interfaces;
using System.IO;
using System.Collections.Generic;

[assembly: Dependency(typeof(FileHelper))]
namespace TestProject.iOS.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}