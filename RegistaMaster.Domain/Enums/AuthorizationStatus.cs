using System.ComponentModel.DataAnnotations;

namespace RegistaMaster.Domain.Enums
{
    public enum AuthorizationStatus
  {
        [Display(Name ="Admin")]
        Admin =0,
        [Display(Name ="Ekip Lideri")]
        TeamLeader =1,
        [Display(Name ="Developer")]
        Developer =2,
    }
}
