using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignInInput
{
    [Display(Name = "Email adresiniz")]
    public string Email { get; set; }

    [Display(Name = "Parolanız")]
    public string Password { get; set; }

    [Display(Name = "Beni hatırla")]
    public bool IsRemember { get; set; }
}
