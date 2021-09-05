using System.Collections.Generic;
using VacationRental.DataAccess.Contracts;

namespace VacationRental.DataAccess
{
    public abstract class RepositoryBase<TClass> : IRepositoryBase<TClass> where TClass : class
    {
        protected readonly VacationApiContext _vacationContext;

        protected RepositoryBase(VacationApiContext context)
        {
            this._vacationContext = context;
        }

        public void Delete(TClass obj)
        {
            _vacationContext.Remove<TClass>(obj);
            _vacationContext.SaveChanges();
        }

        public TClass Get<TKey>(TKey key)
        => _vacationContext.Find<TClass>(key);

        public IEnumerable<TClass> GetAll()
        => _vacationContext.Set<TClass>();

        public void Insert(TClass obj)
        {
            _vacationContext.Add<TClass>(obj);
            _vacationContext.SaveChanges();
        }

        public void Update(TClass obj)
        {
            _vacationContext.Update<TClass>(obj);
            _vacationContext.SaveChanges();
        }
    }
}
