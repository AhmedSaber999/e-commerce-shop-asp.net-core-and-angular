using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Core.Entities.BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

        public IReadOnlyList<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().FirstOrDefault(obj => obj.Id == id);
        }

        public T GetEntityWithSpecification(ISpecification<T> specification)
        {
            return ApplySpecification(specification).FirstOrDefault();       
        }

        public IReadOnlyList<T> GetListWithSpecification(ISpecification<T> specification)
        {
            return ApplySpecification(specification).ToList();
        }

        public int Count(ISpecification<T> specification)
        {
            return ApplySpecification(specification).Count();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specification);
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}