using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Interfaces
{
    public interface IImageService
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
