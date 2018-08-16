using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class RegisterVM
    {
        [StringLength(60, MinimumLength = 5)]
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect passowrd")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Required]
        [Range(0,99)]
        public int DrivingExperience { get; set; }
    }
}
