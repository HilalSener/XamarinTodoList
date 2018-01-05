using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Photo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String DocumentName { get; set; }
        public String DocumentPath { get; set; }
        public MemoryStream DocumentStr { get; set; }
        public byte[] Document { get; set; }
        public int ItemId { get; set; }
        public int SortingId { get; set; }
    }
}
