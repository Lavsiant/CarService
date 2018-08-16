using CarService.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterVM model);

        Task<bool> CheckIfLoginValid(string login);

        Task<IUser> GetUser(LoginVM model);

        Task<Owner> GetOwner(int userId);

        Task<Owner> GetOwnerWithCars(int ownerId);

        Task UpdateAccount(ProfileEditVM profileVM);

        Task DeleteAccount(int id);

        Task<Owner> GetOwner(string name, string surname);

        Task<bool> AddCarToOwner(int ownerId, int carId);
    }
}
