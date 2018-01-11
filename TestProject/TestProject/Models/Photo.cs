using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Utility;
using Xamarin.Forms;

namespace TestProject.Models
{
    public class Photo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String DocumentName { get; set; }
        public String DocumentPath { get; set; }
        public byte[] Document { get; set; }
        public int ItemId { get; set; }
        public int SortingId { get; set; }

        [Ignore]
        public ImageSource DocumentImageSource
        {
            get { return ImageSource.FromStream(() => { return new MemoryStream(Document); }); }
        }
    }
}
