using System.ComponentModel.DataAnnotations;
using System;

namespace TheWall.Models
{
    
    public class User : BaseEntity
    
    {
        
        [Required] 
        [MinLength(2)]    
        public string first_name { get; set; }
     
        [Required] 
        [MinLength(2)]
        public string last_name { get; set; }
    

        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        
        
        
        [Required] 
        [DataType(DataType.Password)]
        [MinLength(8)]

        public string password { get; set; }


        [Required] 
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

    }
}