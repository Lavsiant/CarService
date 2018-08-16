using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    public interface ICarRepository
    {
        Task AddCar(Car car,int ownerId);

        Task<List<Car>> GetAllCars();

        Task<Car> GetCar(int carId);

        Task<Car> GetCarWithOwners(int carId);

        Task UpdateCar(Car car);

        Task DeleteCar(int carId);

        Task<bool> IsUserOwner(int carId, int userId);

        Task<bool> AddCarToOwner(int ownerId, int carId);    
    }
}
