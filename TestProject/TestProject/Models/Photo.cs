using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public String DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public String Document { get; set; }
        public int ItemId { get; set; }
        public int SortingId { get; set; }
    }
}
