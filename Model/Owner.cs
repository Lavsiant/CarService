using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Owner : IUser
    {
        public Owner()
        {
            CarOwners = new List<CarOwner>();
        }

        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
      
        public string Name { get; set; }

        public string Surname { get; set; }

        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        public int DrivingExperience { get; set; }

        public virtual ICollection<CarOwner> CarOwners { get; set; }
    }
}
