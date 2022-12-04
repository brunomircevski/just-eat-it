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

    public byte[] Salt { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime RegistrationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public List<Category> PreferedCategories { get; set; }

    [Display(Name = "Date of Birth")]
    [Range(typeof(DateTime), "1/1/1900", "1/1/2010")]
    [DataType(DataType.Date, ErrorMessage="Date only")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Sex")]
    public bool IsMan { get; set; }

    [Range(30, 250)]
    public float Weight { get; set; }

    [Range(0.89, 1.11)]
    public float Target { get; set; }

    [Range(100, 250)]
    public float Heigth { get; set; }

    /*
    lezacy, praca siedzaca,siedzi + 2 treningi, praca fizyczna + 3 treningi, praca fizyczna + 5, ciezka + 7
    */
    [Range(1, 2)]
    public float Activity { get; set; }

    public int CaloriesDemand()
    {
        double CaloriesCalculate = 0;
        if (IsMan)
        {
            CaloriesCalculate = (66.5 + 13.7 * Weight + 5 * Heigth - 6.8 * (DateTime.Now.Year - BirthDate.Year)) * Activity * Target;
        }
        else
        {
            CaloriesCalculate = (665 + 9.6 * Weight + 1.85 * Heigth - 4.7 * (DateTime.Now.Year - BirthDate.Year)) * Activity * Target;
        }
        return (int) CaloriesCalculate;
    }

    public int Age() {
        return DateTime.Now.Year - BirthDate.Year;
    }

}