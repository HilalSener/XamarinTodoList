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
        public String DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public String Document { get; set; }
        public static List<Photo> Photos { get; set; }

        //public Media media { get; set; }

        //public class Media
        //{
        //    [PrimaryKey, AutoIncrement]
        //    public Int64 Id { get; set; }
        //    public Int64 MediaId { get; set; }
        //    public Byte DocumentTypeId { get; set; }
        //    public Guid UniqeId { get; set; }
        //    public String DocumentName { get; set; }
        //    public String Document { get; set; }
        //    public bool? IsDraft { get; set; }
        //    public string DocumentPath { get; set; }
        //    public bool IsUrl { get; set; }
        //    public string Url { get; set; }
        //    public string MediaIcon { get; internal set; }
        //}

        //int id;
        //string title;
        //string description;
        //DateTime articleDate;

        //[PrimaryKey, AutoIncrement]
        //[JsonProperty(PropertyName = "id")]
        //public int Id {
        //    get { return id; }
        //    set { id = value; }
        //}

        //[MaxLength(100)]
        //[JsonProperty(PropertyName = "title")]
        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; }
        //}

        //[JsonProperty(PropertyName = "description")]
        //public string Description
        //{
        //    get { return description; }
        //    set { description = value; }
        //}

        //[JsonProperty(PropertyName = "articleDate")]
        //public DateTime ArticleDate
        //{
        //    get { return articleDate; }
        //    set { articleDate = value; }
        //}
    }
}
