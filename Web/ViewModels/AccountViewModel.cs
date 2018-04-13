using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Domains;

namespace Web.ViewModels
{
    public class AccountViewModel
    {
        [Required]
        public int Id { get; set; }


        [MaxLength(200)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        public AccountViewModel(Account account)
        {
            Id = user.Id;
            EmailId = user.EmailId;
            IsActive = user.IsActive;
            Password = user.PasswordHash;
            ListOfFacilitiesAssigned = user.Facilities;
        }

        public UserViewModel()
        {

        }
    }
}