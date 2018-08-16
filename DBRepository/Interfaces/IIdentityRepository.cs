using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    public interface IIdentityRepository 
    {    
        Task<IUser> GetUser(string login, string password);

        Task<IUser> GetUser(string login);     
    }
}
