using System.ComponentModel.DataAnnotations;

namespace JEI.Models; 

public enum Units
{
    [Display(Name = "100 g")]
    g,
    [Display(Name = "100 ml")]
    ml,
    [Display(Name = "Item")]
    item
}

public static class Translate {
    public static string Units(Units u) {
        switch(u) {
            case Models.Units.g: return "100 g";
            case Models.Units.ml: return "100 ml";
            case Models.Units.item: return "item";
        }
        return null;
    }
}