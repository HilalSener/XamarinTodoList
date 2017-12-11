using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Interfaces;

namespace TestProject.Helpers
{
    public static class StorageHelper
    {
        public static string GetLocalFilePath()
        {
            return Xamarin.Forms.DependencyService.Get<IFileHelper>().GetLocalFilePath("test.db3");
        }
    }
}
