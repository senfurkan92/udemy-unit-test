using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyUnitTest.Dal.Models;

namespace UdemyUnitTest.Dal.Data
{
	public interface IRepository<TEntity> where TEntity : class
	{ 
		Task<TEntity> GetByIdAsync(int id);

		Task<IEnumerable<TEntity>> GetAllAsync();

		Task<TEntity> Create(TEntity entity);

		Task<int> Update(TEntity entity);

		Task<int> DeleteByIdAsync(TEntity entity);
	}

	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{

		private readonly BikeStoreDbContext _dbContext;
		private readonly DbSet<TEntity> _dbSet;

		public Repository(BikeStoreDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<TEntity>();
		}

		public async Task<TEntity> Create(TEntity entity)
		{
			var result = await _dbSet.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<int> DeleteByIdAsync(TEntity entity)
		{
			_dbContext.Entry(entity).State= EntityState.Deleted;
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			_dbContext.Entry(entity).State = EntityState.Detached;
			return entity;
		}

		public async Task<int> Update(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			return await _dbContext.SaveChangesAsync();
		}
	}
}
