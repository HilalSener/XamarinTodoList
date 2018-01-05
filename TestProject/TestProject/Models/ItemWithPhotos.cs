using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class ItemWithPhotos
    {
        public ItemWithPhotos()
        {
            Photos = Photos ?? new List<Photo>();
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ArticleDate { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
