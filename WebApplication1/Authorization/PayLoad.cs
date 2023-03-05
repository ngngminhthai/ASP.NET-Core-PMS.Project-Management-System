using PMS.Authorization;

namespace RBAC.Application.Authorization
{
    public class Payload
    {
        public string Resource { get; set; }
        public ProjectRequirement ProjectRequirement { get; set; }
        public ConversationRequirement ConversationRequirement { get; set; }

    }
}
