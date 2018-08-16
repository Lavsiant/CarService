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
    public class CarService : ICarService
    {
        ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task AddCar(CarCreateVM carVM, string ownerId)
        {
            var car = new Car() { Model = carVM.Model, Price = carVM.Price, ReleaseDate = carVM.ReleaseDate, Series = carVM.Series };
            // var carOwner = new CarOwner() { CarId = car.ID, OwnerId = Convert.ToInt32(ownerId.ToString()) };
            await _carRepository.AddCar(car, Convert.ToInt32(ownerId));
        }

        public async Task<bool> AddCarToOwner( int ownerId, int carId)
        {
            return await _carRepository.AddCarToOwner(ownerId, carId);
        }

        public async Task<List<Car>> GetAllCars()
        {
            return await _carRepository.GetAllCars();
        }

        public async Task<Car> GetCar(int id)
        {
            return await _carRepository.GetCar(id);
        }

        public async Task<Car> GetCarWithOwners(int carId)
        {
            return await _carRepository.GetCarWithOwners(carId);
        }

        public async Task<bool> IsCarExist(int carId)
        {
            var car = await _carRepository.GetCar(carId);
            return car != null;
        }

        public async Task<bool> IsUserOwner(int carId, int userId)
        {
            return await _carRepository.IsUserOwner(carId, userId);

        }

        public async Task UpdateCar(Car car)
        {
            await _carRepository.UpdateCar(car);
        }

        public async Task DeleteCar(int carId)
        {
            await _carRepository.DeleteCar(carId);
        }

        public List<Car> FilterCarList(List<Car> cars, string model, string series, string type)
        {
            if (!String.IsNullOrEmpty(model))
            {
                 cars = cars.Where(x => x.Model.ToLower().Contains(model.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(series))
            {
                cars = cars.Where(x => x.Series.ToLower().Contains(series.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(type))
            {
                CarType carType;
                Enum.TryParse(type, out carType);

                cars = cars.Where(x => x.Type == carType).ToList();
            }
            return cars;
        }      
    }
}
