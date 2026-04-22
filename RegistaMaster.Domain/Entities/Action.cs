using RegistaMaster.Domain.Enums;
using System.ComponentModel;

namespace RegistaMaster.Domain.Entities
{
    public class Action : BaseEntity
    {
        [DisplayName("Aksiyon Konusu")]
        public string Subject { get; set; }
        [DisplayName("Sorumlu")]
        public int ResponsibleID { get; set; }
        public User Repsonsible { get; set; }
        [DisplayName("Açıklama Tarihi")]
        public DateTime OpeningDate { get; set; }
        [DisplayName("Son Tarih")]
        public DateTime EndDate { get; set; }
        [DisplayName("Başlama Tarihi")]
        public DateTime StartDate { get; set; }
        [DisplayName("Tamamlama Tarihi")]
        public DateTime ComplateDate { get; set; }
        public string? Description { get; set; }
        [DisplayName("Durum")]
        public ActionStatus ActionStatus { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public ActionPriorityStatus ActionPriorityStatus { get; set; }
        public int RequestID { get; set; }
        public Request Request { get; set; }
        public ICollection<ActionNote>? ActionNotes { get; set; }
    }
}
