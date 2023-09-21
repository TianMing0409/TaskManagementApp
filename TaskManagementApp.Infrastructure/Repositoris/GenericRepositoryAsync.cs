using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Infrastructure.Persistence.Contexts;

namespace TaskManagementApp.Infrastructure.Persistence.Repositoris
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly TaskManagementDbContext _taskDbContext;

        public GenericRepositoryAsync(TaskManagementDbContext taskDbContext)
        { 
            _taskDbContext = taskDbContext; 
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _taskDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int pagenNmber, int pageSize)
        {
            return await _taskDbContext
                .Set<T>()
                .Skip((pagenNmber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        { 
            await _taskDbContext.AddAsync(entity);
            await _taskDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        { 
            _taskDbContext.Entry(entity).State = EntityState.Modified;
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        { 
            _taskDbContext.Remove(entity);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        { 
            return await _taskDbContext
                .Set<T>()
                .ToListAsync ();
        }

    }
    
}
