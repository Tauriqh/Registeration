using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Registeration.Data;
using Registeration.Models;

namespace Registeration.Pages.ApplicationUsers
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationUserContext _context;

        public LoginModel(ApplicationUserContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Display(Name = "Email Address"), DataType(DataType.EmailAddress), Required]
        public string EmailAddressString { get; set; }

        public string EmailAddressErrorMessage { get; set; }

        [BindProperty, Display(Name = "Password")]
        [DataType(DataType.Password), Required]
        public string PasswordString { get; set; }

        public string PasswordErrorMessage { get; set; }

        public IActionResult OnGet()
        {
           return Page();
        }

        //[BindProperty]
        //public ApplicationUser ApplicationUser { get; set; }

        public IActionResult OnPost()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/
            
            try
            {
                // The Contains method is run on the database
                var email = (from u in _context.ApplicationUser
                                where (u.EmailAddress == EmailAddressString)
                                select u.EmailAddress).Single();
            }
            catch (InvalidOperationException)
            {
                EmailAddressErrorMessage = "An account with this Email Address does not exist!!";
                return Page();
            }

            try
            {
                string encryptedPass = EncryptPassword(PasswordString);
                
                var password = (from u in _context.ApplicationUser
                                where (u.EmailAddress == EmailAddressString && u.Password == encryptedPass)
                                select u.Password).Single();
            }
            catch (InvalidOperationException)
            {
                PasswordErrorMessage = "Invalid Password!!";
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public string EncryptPassword(string password)
        {
            HashString hash = new HashString();
            string hashPassword = hash.ComputeSha256Hash(password);
            
            return hashPassword;
        }
    }
}
  