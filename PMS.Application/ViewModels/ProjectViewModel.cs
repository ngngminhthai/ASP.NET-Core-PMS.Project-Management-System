using PMS.Data.Entities;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Tag Tag { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ProjectCommentViewModel>? ListComment { get; set; }

    }
}
