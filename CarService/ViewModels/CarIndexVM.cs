using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CarIndexVM
    {
        public List<Car> Cars { get; set; }

        public string СarType { get; set; }

        public SelectList СarTypeList { get; set; }
    }
}
