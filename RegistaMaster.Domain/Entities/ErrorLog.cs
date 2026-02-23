using System.ComponentModel;

namespace RegistaMaster.Domain.Entities
{
    public class ErrorLog:BaseEntity
    {
        [DisplayName("Kullanıcı Adı ve Soyadı")]
        public string NameSurname { get; set; }
        [DisplayName("Hata Tarihi")]
        public DateTime ErrorDate { get; set; }
        [DisplayName("Hata Mesajı")]
        public string ErrorDesc { get; set; }
        [DisplayName("Müşteri")]
        public int ClientId { get; set; }
        [DisplayName("Kullanıcı")]
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
