using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebShop.Repositories
{
	public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		readonly DbContext _context;
		DbSet<TEntity> _dbSet;

		public EFGenericRepository(DbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public void Create(TEntity item)
		{
			_dbSet.Add(item);
			_context.SaveChanges();
		}

		public TEntity FindById(Guid id)
		{
			return _dbSet.Find(id);
		}

		public TEntity Find(string username, string password)
		{
			return _dbSet.FirstOrDefault();
		}

		public IEnumerable<TEntity> Get()
		{
			return _dbSet.AsNoTracking().ToList();
		}

		public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
		{
			return _dbSet.AsNoTracking().Where(predicate).ToList();
		}

		public void Remove(TEntity item)
		{
			_dbSet.Remove(item);
			_context.SaveChanges();
		}

		public void Update(TEntity item)
		{
			_context.Entry(item).State = EntityState.Modified;
			_context.SaveChanges();
		}

		internal void Remove(Func<object, bool> p)
		{
			throw new NotImplementedException();
		}
	}
}
