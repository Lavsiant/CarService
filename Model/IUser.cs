using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IUser
    {
         string Login { get; set; }
         string Password { get; set; }
         int ID { get; set; }
    }
}
