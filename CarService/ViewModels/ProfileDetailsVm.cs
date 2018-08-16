using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class ProfileDetailsVm
    {
        public Owner Owner { get; set; }

        public bool IsValidForEditing { get; set; }
    }
}
