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
    public class IdentityRepository : BaseRepository, IIdentityRepository
    {
        public IdentityRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public async Task<IUser> GetUser(string login)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Owners.FirstOrDefaultAsync(u => u.Login == login);
            }
        }

        public async Task<IUser> GetUser(string login,string password)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Owners.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
            }
        }
    }
}
