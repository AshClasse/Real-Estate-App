using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Infrastructure.Persistence.Repositories.Generic;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropertyRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Property>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _dbContext.Set<Property>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.Where(q => !q.IsDeleted).ToListAsync();
        }
    }
}
