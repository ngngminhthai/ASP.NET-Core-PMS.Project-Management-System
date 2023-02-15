using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<ProjectCommentViewModel>? ListComment { get; set; }

    }
}
