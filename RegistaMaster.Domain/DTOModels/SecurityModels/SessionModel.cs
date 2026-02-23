using RegistaMaster.Domain.Enums;

namespace RegistaMaster.Domain.DTOModels.SecurityModels
{
    public class SessionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname => Name + " "  + Surname;
        public string Image { get; set; }
        public int  CustomerId { get; set; }
        public AuthorizationStatus Authorization{ get; set; }
    }
}
