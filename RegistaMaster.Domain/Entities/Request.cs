using RegistaMaster.Domain.Enums;
using System.ComponentModel;

namespace RegistaMaster.Domain.Entities
{
    public class Request : BaseEntity
    {
        [DisplayName("Konu")]
        public string Subject { get; set; }
        [DisplayName("Açıklama")]
        public string? Description { get; set; }
        [DisplayName("Kategori")]
        public string? Category { get; set; }
        public int? CategoryID { get; set; }
        [DisplayName("Bildirim Türü")]
        public string? NotificationType { get; set; }
        public int? NotificationTypeID { get; set; }
        [DisplayName("Sayfa Linki")]
        public string? PageUrl { get; set; }
        [DisplayName("Görüntü")]
        public string? PictureUrl { get; set; }
        [DisplayName("Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }
        [DisplayName("Bitiş Tarihi")]
        public DateTime PlanedEndDate { get; set; }
        [DisplayName("Durum")]
        public RequestStatus RequestStatus { get; set; }
        [DisplayName("Bildirim ID")]
        public int NotificationID { get; set; }
        [DisplayName("Versiyon")]
        public int? VersionID { get; set; }
        public Version Version { get; set; }
        public int? ModuleID { get; set; }
        public int ProjectID { get; set; }
        public Project Project { get; set; }
        public ICollection<Action> Actions { get; set; }
        public ICollection<RequestFile> Files { get; set; }
    }
}
