using System;

namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public UserViewModel() { }
        public UserViewModel(RegistrationForm registrationInfo)
        {
            Id = Guid.NewGuid();
            RegistrationInfo = registrationInfo;
        }

        public Guid Id { get; set; }
        public RegistrationForm RegistrationInfo { get; set; }
        public string? Name { get; set; }   
        public string? Surname { get; set; }    

        public override bool Equals(object obj)
        {
            return obj is UserViewModel user &&
                   RegistrationInfo.Equals(user.RegistrationInfo);
        }
    }
}
