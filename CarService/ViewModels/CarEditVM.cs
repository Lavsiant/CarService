using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CarEditVM
    {
        public CarEditVM()
        {
            var categoryQuery = Enum.GetNames(typeof(CarType));
            CarTypes = new SelectList(categoryQuery);
        }

        public Car Car { get; set; }

        public SelectList CarTypes { get; set; }
    }
}
