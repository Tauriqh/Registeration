using System.ComponentModel.DataAnnotations;

namespace Registeration.Models
{
    public class ApplicationUser
    {
        public int ID { get; set; }

        // must be unique
        [Display(Name = "Email Address")]
        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "First Name")]
        [Required, RegularExpression(@"^[A-Z]+[a-zA-Z]*$")] //Must only use letters. The first letter is required to be uppercase.White space, numbers, and special characters are not allowed.
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required, RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string LastName { get; set; }

        // must be encrypted before being saved
        // must have at least 1 uppercase character, 1 lowercase character, 1 special character,1 number and must be at least 6 characters long and max 15
        [Required, DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$")]
        public string Password { get; set; }
    }
}
