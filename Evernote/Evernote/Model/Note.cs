using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evernote.Model
{
    public interface HasId
    {
        public string Id { get; set; }
    }
    public class Note : HasId
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        //[Indexed]
        public string Id { get; set; }
        public string NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string FileLocation { get; set; }
    }
}
