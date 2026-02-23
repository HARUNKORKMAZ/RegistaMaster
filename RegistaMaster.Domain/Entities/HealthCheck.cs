using System.ComponentModel;

namespace RegistaMaster.Domain.Entities
{
    public class HealthCheck : BaseEntity
    {
        [DisplayName("Durum")]
        public string RequestStatus { get; set; }
        [DisplayName("Açıklama")]
        public string RequestDesc { get; set; }
        [DisplayName("Tarih")]
        public DateTime RequestDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }


    }
}
