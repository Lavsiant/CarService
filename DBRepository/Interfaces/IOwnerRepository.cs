using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    public interface IOwnerRepository
    {
        Task<Owner> GetOwner(int id);

        Task<Owner> GetOwnerWithCars(int ownerId);

        Task UpdateOwner(Owner owner);

        Task DeleteOwner(int id);

        Task<Owner> GetOwner(string name, string surname);

        Task AddOwner(Owner user);

        Task<bool> AddCarToOwner(int ownerId, int carId);
    }
}
