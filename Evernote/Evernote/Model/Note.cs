using System;
using System.Collections.Generic;
using System.Text;

namespace Evernote.Model
{
    public class Note
    {
        public int Id { get; set; }
        public int NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string FileLocation { get; set; }
    }
}
