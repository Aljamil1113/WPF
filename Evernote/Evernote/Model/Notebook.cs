using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evernote.Model
{
    public class Notebook : HasId
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        //[Indexed]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
