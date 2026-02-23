using System.ComponentModel;

namespace RegistaMaster.Domain.Entities
{
    public class UserLog : BaseEntity
    {
        [DisplayName("Ad ve Soyad")]
        public string NameSurname { get; set; }
        [DisplayName("Giriş Yapılan Tarih")]
        public DateTime LoginDate { get; set; }
        [DisplayName("Müşteri Id")]
        public int? ClientId { get; set; }
        [DisplayName("Kullanıcı Id")]
        public int? MemberId { get; set; }
        public int ProjectId { get; set; }
        public Project Projects { get; set; }
    }
}
