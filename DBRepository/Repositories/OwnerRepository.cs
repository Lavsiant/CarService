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
    public class OwnerRepository : BaseRepository, IOwnerRepository
    {
        public OwnerRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }


        public async Task AddOwner(Owner owner)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Owners.Add(owner);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Owner> GetOwnerWithCars(int ownerId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Owners.Include(x => x.CarOwners).ThenInclude(x=>x.Car).FirstOrDefaultAsync(x => x.ID == ownerId);
            }
        }

        public async Task<Owner> GetOwner(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Owners.FirstOrDefaultAsync(u => u.ID == id);
            }
        }

        public async Task<Owner> GetOwner(string name, string surname)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Owners.FirstOrDefaultAsync(u => u.Surname == surname && u.Name == name);
            }
        }

        public async Task<bool> AddCarToOwner(int ownerId, int carId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                Owner owner = await GetOwnerWithCars(ownerId);
                if (owner.CarOwners.Any(x => x.Car.ID == carId))
                {
                    return false;
                }

                var car = context.Cars.FirstOrDefault(x => x.ID == carId);
                owner.CarOwners.Add(new CarOwner() { Car = car,Owner = owner});
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task UpdateOwner(Owner owner)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Update(owner);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteOwner(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var owner = await GetOwner(id);

                context.Owners.Remove(owner);

                await context.SaveChangesAsync();
            }
        }
    }
}
