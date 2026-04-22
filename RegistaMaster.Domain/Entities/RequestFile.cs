namespace RegistaMaster.Domain.Entities
{
    public class RequestFile : BaseEntity
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int RequestID { get; set; }
    }
}
