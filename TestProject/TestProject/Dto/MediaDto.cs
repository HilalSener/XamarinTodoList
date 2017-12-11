using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Dto
{
    public class MediaDto
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }
        public Int64 MediaId { get; set; }
        public Byte DocumentTypeId { get; set; }

        public Guid UniqeId { get; set; }

        public String DocumentName { get; set; }
        public String Document { get; set; }
        public bool? IsDraft { get; set; }
        public string DocumentPath { get; set; }
        public bool IsUrl { get; set; }
        public string Url { get; set; }
        public string MediaIcon { get; internal set; }
    }
}
