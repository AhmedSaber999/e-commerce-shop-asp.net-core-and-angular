using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext Context;
        private Hashtable repositories;
        public UnitOfWork(StoreContext context)
        {
            Context = context;
        }


        public async Task<int> Complete()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(repositories == null) repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), Context);
                repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)repositories[type];
        }
    }
}