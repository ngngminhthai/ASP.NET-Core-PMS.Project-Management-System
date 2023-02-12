namespace PMS.Data.Entities.ValueObjects
{
    public class WorkingStatus
    {
        public string Name { get; set; }

        public WorkingStatus(string name)
        {
            Name = name;
        }

        public static WorkingStatus InProgress { get { return new WorkingStatus("In Progress"); } }
        public static WorkingStatus Done { get { return new WorkingStatus("Done"); } }

    }
}
