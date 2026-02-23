using System.ComponentModel.DataAnnotations;

namespace RegistaMaster.Domain.Enums
{
    public enum ActionStatus
    {
        [Display(Name = "Başlamadı")]
        NotStarted = 0,
        [Display(Name = "Devam Ediyor")]
        Continued = 1,
        [Display(Name = "Tamamlandı")]
        Completed = 2,
        [Display(Name = "İptal/Reddedildi")]
        Canceled = 3
    }
}
