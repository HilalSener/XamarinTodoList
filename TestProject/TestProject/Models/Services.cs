using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Services
    {
        public Services()
        {
            Images = Images ?? new List<string>();
        }
        public string Descripition { get; set; }
        public int SectionId { get; set; }
        public int TroubleCategoryId { get; set; }
        public List<string> Images { get; set; }
    }
}
