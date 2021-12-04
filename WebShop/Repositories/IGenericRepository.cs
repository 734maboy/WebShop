using System;
using System.Collections.Generic;

namespace WebShop.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class 
	{
		void Create(TEntity item);
		TEntity FindById(Guid id);
		TEntity Find(string username, string password);
		IEnumerable<TEntity> Get();
		IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
		void Remove(TEntity item);
		void Update(TEntity item);
	}
}
