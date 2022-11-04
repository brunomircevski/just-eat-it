using System.ComponentModel.DataAnnotations;

namespace JEI.Models; 

public class LoginUserForm 
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    [MaxLength(100)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Powtórz Hasło")]
    [MaxLength(100)]
    public string Password2 { get; set; }
}