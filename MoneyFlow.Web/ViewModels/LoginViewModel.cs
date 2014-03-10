using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Web.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}