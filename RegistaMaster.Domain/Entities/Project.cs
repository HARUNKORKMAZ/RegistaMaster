using System.ComponentModel;

namespace RegistaMaster.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            ProjectGuid = Guid.NewGuid();
        }
        public Guid ProjectGuid { get; set; }
        [DisplayName("Proje Adı")]
        public string ProjectName { get; set; }
        [DisplayName("Proje Açıklaması")]
        public string ProjectDescription { get; set; }
        public ICollection<ProjectNote> ProjectNotes { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
