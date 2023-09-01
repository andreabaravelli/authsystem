using System.ComponentModel.DataAnnotations;

namespace SeatsProject.Models
{
    public class UserLogin
    {
        [Key]
        public int id { get; set; }
        [Required (ErrorMessage = "Please Enter Username")]
        [Display(Name = "Please Enter Password")]
        public string userName { get; set; }
        [Required (ErrorMessage= "Please Enter Password")]
        [Display(Name ="Please Enter Password")]
        public string passcode { get; set; }
        public int isActive { get; set; }
    }
}
