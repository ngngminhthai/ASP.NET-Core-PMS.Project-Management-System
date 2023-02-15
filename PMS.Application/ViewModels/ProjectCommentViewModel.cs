using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ProjectCommentViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int Level { get; set; }
        public int? ParentID { get; set; }
        public int NumberOfLike { get; set; }
        public List<ProjectCommentViewModel>? ChildComments { get; set; }

    }
}
