using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CarOwner
    {
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
