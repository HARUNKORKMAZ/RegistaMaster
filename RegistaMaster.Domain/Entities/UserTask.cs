using RegistaMaster.Domain.Enums;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace RegistaMaster.Domain.Entities
{
  public class UserTask : BaseEntity
  {
    [DisplayName("Konu")]
    public string Subject { get; set; }
    [DisplayName("Açıklama")]
    public string Description { get; set; }
    public string? Category { get; set; }
    public int? CetegoryId { get; set; }
    [DisplayName("Sayfa Linki")]
    public string? PageUrl { get; set; }
    [DisplayName("Başlangıç Tarihi")]
    public DateTime StartDate { get; set; }
    [DisplayName("Bitiş Tarihi")]
    public DateTime PlannedEndDate { get; set; }
    [DisplayName("Durum")]
    public RequestStatus RequestStatus { get; set; }
    [DisplayName("Versiyon")]
    public int? VersionId { get; set; }
    public Version Version { get; set; }
    public int? ModuleId { get; set; }
    public int ProjetId { get; set; }
    public Project Project { get; set; }




  }
}
