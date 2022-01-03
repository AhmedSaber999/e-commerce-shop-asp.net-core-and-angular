using System.Collections.Generic;
using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IReadOnlyList<T> GetAll();
        T GetEntityWithSpecification(ISpecification<T> specification);
        IReadOnlyList<T> GetListWithSpecification(ISpecification<T> specification);
        int Count(ISpecification<T> specification);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}