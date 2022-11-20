using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEI.Models; 

public class User 
{
    [Required]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }

    [Required]
    public byte[] Salt { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime RegistrationDate { get; set; }
    public DateTime ?LastLoginDate { get; set; }
}