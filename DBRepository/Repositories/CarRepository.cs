using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public async Task AddCar(Car car, int ownerId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var owner = await context.Owners.Include(x => x.CarOwners).ThenInclude(x => x.Car).FirstOrDefaultAsync(x => x.ID == ownerId);
                owner.CarOwners.Add(new CarOwner { Car = car, Owner = owner });
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Car>> GetAllCars()
        {
            var carList = new List<Car>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                carList = await context.Cars.ToListAsync();
            }

            return carList;
        }

        public async Task<Car> GetCar(int carId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Cars.FirstOrDefaultAsync(x => x.ID == carId);
            }
        }

        public async Task<Car> GetCarWithOwners(int carId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var car = await context.Cars.Include(x => x.CarOwners).ThenInclude(x => x.Owner).FirstOrDefaultAsync(x => x.ID == carId);
                return car;
            }
        }

        public async Task UpdateCar(Car car)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Update(car);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteCar(int carId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var car = await GetCar(carId);

                context.Cars.Remove(car);

                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> AddCarToOwner(int ownerId, int carId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                Owner owner = await context.Owners.Include(x => x.CarOwners).ThenInclude(x => x.Car).FirstOrDefaultAsync(x => x.ID == ownerId);
                if (owner.CarOwners.Any(x => x.Car.ID == carId))
                {
                    return false;
                }

                var car = context.Cars.FirstOrDefault(x => x.ID == carId);
                owner.CarOwners.Add(new CarOwner() { Car = car, Owner = owner });
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> IsUserOwner(int carId, int userId)
        {
            bool result = false;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var car = await GetCarWithOwners(carId);
                if (car.CarOwners.Any(x => x.CarId == carId && x.OwnerId == userId))
                {
                    result = true;
                }
            }
            return result;
        }

        
    }
}
