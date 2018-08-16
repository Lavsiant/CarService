using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CarOwnerAddVM
    {
        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 1)]
        public string Surname { get; set; }

        public int carId { get; set; }
    }
}
