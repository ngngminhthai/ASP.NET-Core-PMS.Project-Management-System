namespace PMS.Authorization
{
    public class ProjectRequirement
    {
        public int ProjectId { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
    }
}
