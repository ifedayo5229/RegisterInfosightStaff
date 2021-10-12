using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace register.Entities
{
    public class ApplicationUser : IdentityUser

    {
        [Required]
          public string FirstName { get; set; }
         
        [Required]
         public string LastName { get; set; }
          public decimal Salary { get; set; }
        
         [Required]
    
        public decimal GrossIncome { get; set; }

         [Required]
    
        public decimal NetIncome { get; set; }
    }
}