namespace RegistaMaster.Domain.Entities
{
    public class ActionNote : BaseEntity
    {
        public int ActionID { get; set; }
        public Action Action { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
