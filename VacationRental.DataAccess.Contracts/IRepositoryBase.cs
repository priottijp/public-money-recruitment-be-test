using System.Collections.Generic;

namespace VacationRental.DataAccess.Contracts
{
    public interface IRepositoryBase<TClass> where TClass : class
    {
        TClass Get<TKey>(TKey key);
        IEnumerable<TClass> GetAll();
        void Insert(TClass obj);
        void Update(TClass obj);
        void Delete(TClass obj);
    }

}
