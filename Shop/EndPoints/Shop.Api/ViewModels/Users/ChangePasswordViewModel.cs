using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Users;

public class ChangePasswordViewModel
{
    [DisplayName("کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string CurrentPassword { get; set; }

    [DisplayName("کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    public string Password { get; set; }

    [DisplayName("تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare("Password", ErrorMessage = "کلمه های عبور یکسان نیستند")] 
    public string ConfirmPassword { get; set; }
}