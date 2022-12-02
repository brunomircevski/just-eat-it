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

    public List<Category> PreferedCategories { get; set; }

    public DateTime BirthDate { get; set; }

    public bool IsMan { get; set; }

    [Range(30,250)]
    public float Weight { get; set; }
    
    [Range(0.9,1.1)]
    public int Target { get; set; } 
    
    [Range(100,250)]
    public float Height { get; set; }
    
    /*
     lezacy, praca siedzaca,siedzi + 2 treningi, praca fizyczna + 3 treningi, praca fizyczna + 5, ciezka + 7
     */
    [Range(1,2)]
    public float Activity { get; set; }

    public int CaloriesDemand()
    {
        float CaloriesCalculate = 0;
        if (IsMan)
        {
            CaloriesCalculate = (66,5 + 13,7*Weight+5*Height - 6,8* (DateTime.Now.Year-BirthDate.Year))*Activity*Target;
        }
        else
        {
            CaloriesCalculate = (665+9.6*Weight+1.85*Height-4.7*(DateTime.Now.Year-BirthDate.Year))*Activity*Target; 
        }
        return (int)CaloriesCalculate;
    }

    



}