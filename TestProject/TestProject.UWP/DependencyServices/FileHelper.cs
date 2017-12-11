using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Interfaces;
using TestProject.UWP.DependencyServices;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TestProject.UWP.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
