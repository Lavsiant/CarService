using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CarCreateVM
    {
        public CarCreateVM()
        {
            var categoryQuery = Enum.GetNames(typeof(CarType));
            CarTypes = new SelectList(categoryQuery);
        }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Model { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Series { get; set; }

        [DataType(DataType.Currency)]
        [Display]
        public decimal Price { get; set; }

        public CarType Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public SelectList CarTypes { get; set; }
    }
}
