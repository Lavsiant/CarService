using CarService.Services.Interfaces;
using CarService.ViewModels;
using DBRepository.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services.Implementation
{
    public class AccountService : IAccountService
    {
        IIdentityRepository _identityRepository;
        IOwnerRepository _ownerRepository;

        public AccountService(IIdentityRepository identityRepository,IOwnerRepository ownerRepository)
        {
            _identityRepository = identityRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task RegisterUser(RegisterVM model)
        {
            var owner = new Owner() { Login = model.Login, Password = model.Password,
                BornDate = model.BornDate, DrivingExperience = model.DrivingExperience,
                Name = model.Name, Surname = model.Surname };
            await _ownerRepository.AddOwner(owner);
        }

        public async Task<bool> CheckIfLoginValid(string login)
        {
            return  await _identityRepository.GetUser(login) == null;
            
        }

        public async Task<IUser> GetUser(LoginVM model)
        {
            return await _identityRepository.GetUser(model.Login, model.Password);
        }

        public async Task<Owner> GetOwner(int userId)
        {
            return await _ownerRepository.GetOwner(userId);
        }

        public async Task<Owner> GetOwnerWithCars(int ownerId)
        {
            return await _ownerRepository.GetOwnerWithCars(ownerId);
        }

        public async Task UpdateAccount(ProfileEditVM profileVM)
        {
            var owner = new Owner()
            {
                ID = profileVM.ID,
                Name = profileVM.Name,
                Surname = profileVM.Surname,
                BornDate = profileVM.BornDate,
                DrivingExperience = profileVM.DrivingExperience,
                Password = profileVM.Password,
                Login = profileVM.Login
            };
            await _ownerRepository.UpdateOwner(owner);
        }

        public async Task DeleteAccount(int id)
        {
           await _ownerRepository.DeleteOwner(id);
        }

        public async Task<Owner> GetOwner(string name, string surname)
        {
            return await _ownerRepository.GetOwner(name, surname);
        }

        public async Task<bool> AddCarToOwner(int ownerId,int carId)
        {
            return await _ownerRepository.AddCarToOwner(ownerId, carId);
        }

    }
}
