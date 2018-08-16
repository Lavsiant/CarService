using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Car
    {
        public Car()
        {
            CarOwners = new List<CarOwner>();
        }

        public int ID { get; set; }

        public string Model { get; set; }

        public string Series { get; set; }

        public decimal Price { get; set; }

        public CarType Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

     
        public virtual ICollection<CarOwner> CarOwners { get; set; }
    }
}
