using System.ComponentModel.DataAnnotations;

namespace JEI.Models; 

public class LoginUser
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [MaxLength(100)]
    [MinLength(8)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Repeat Password")]
    [MaxLength(100)]
    [MinLength(8)]
    public string Password2 { get; set; }
}