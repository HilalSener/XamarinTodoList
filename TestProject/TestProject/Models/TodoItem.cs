using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.DAL
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ArticleDate { get; set; }


        //public String DocumentName { get; set; }
        //public string DocumentPath { get; set; }
        //public String Document { get; set; }
        //public static List<Photo> Photos { get; set; }
    }
}
