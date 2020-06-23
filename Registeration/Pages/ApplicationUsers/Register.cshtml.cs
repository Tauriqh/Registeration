using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Registeration.Data;
using Registeration.Models;

namespace Registeration.Pages.ApplicationUsers
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationUserContext _context;

        public RegisterModel(ApplicationUserContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        //check vid on registration using identoty @ 20:20min for how to compare attribute (password with compare password)
        [BindProperty, Display(Name = "Confirm Password")]
        [DataType(DataType.Password), Required, RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$")]
        public string ConfirmPassword { get; set; }

        public string ConfirmPasswordErrorMessage { get; set; }

        public string EmailAddressErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        { 
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ApplicationUser.Password.Equals(ConfirmPassword))
            {
                ConfirmPasswordErrorMessage = "Passwords does not match!!";
            }
            else
            {
                try
                {
                    // The Contains method is run on the database
                    var email = (from u in _context.ApplicationUser
                                 where (u.EmailAddress == ApplicationUser.EmailAddress)
                                 select u.EmailAddress).Single();

                    EmailAddressErrorMessage = "An account with this Email Address already exists!!";
                    return Page();
                }
                catch (InvalidOperationException)
                {
                    string encryptedPass = EncryptPassword(ApplicationUser.Password);
                    ApplicationUser.Password = encryptedPass;

                    _context.ApplicationUser.Add(ApplicationUser);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
            }

            return Page();
        }

        public string EncryptPassword(string password)
        {
            HashString hash = new HashString();
            string hashPassword = hash.ComputeSha256Hash(password);
            
            return hashPassword;
        }
    }
}