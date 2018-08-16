using CarService.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services.Interfaces
{
    public interface ICarService
    {
        Task AddCar(CarCreateVM carVM, string ownerLogin);

        Task<bool> AddCarToOwner(int ownerId, int carId);

        Task<List<Car>> GetAllCars();

        Task<Car> GetCar(int carId);

        Task<Car> GetCarWithOwners(int carId);

        Task<bool> IsCarExist(int carId);

        Task<bool> IsUserOwner(int carId,int userId);

        Task UpdateCar(Car car);

        Task DeleteCar(int carId);

        List<Car> FilterCarList(List<Car> cars, string model, string series, string type);       
    }
}
