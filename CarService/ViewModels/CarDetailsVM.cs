using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CarDetailsVM
    {
        public Car Car { get; set; }

        public bool IsUserValidForEditing { get; set; }
     
    }
}
