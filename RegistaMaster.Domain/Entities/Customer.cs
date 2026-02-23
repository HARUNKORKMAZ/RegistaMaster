using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RegistaMaster.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [DisplayName("Name")]
        [StringLength(150)]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden büyük olamaz")]
        public string Name { get; set; }

        [DisplayName("Adres")]
        [StringLength(600)]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz")]
        [MaxLength(600, ErrorMessage = "{0} {1} karakterden büyük olamaz")]
        public string? Address { get; set; }
        [DisplayName("Email")]
        public string? Email { get; set; }
        public string? ApiKey { get; set; }
        [DisplayName("Müşteri Tanım No")]
        public int? CustomerDescriptionId { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<ProjectNote> ProjectNotes { get; set; }



    }
}
