namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectUser
    {
        public string UserId { get; set; }
        public ManageUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
