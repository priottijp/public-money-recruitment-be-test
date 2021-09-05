using System.Collections.Generic;
using System.Linq;

namespace VacationRental.DataAccess.Contracts
{
    public interface IRepositoryBase<TClass> where TClass : class
    {
        TClass Get<TKey>(TKey key);
        IQueryable<TClass> GetAll();
        void Insert(TClass obj);
        void Update(TClass obj);
        void Delete(TClass obj);
    }

}
