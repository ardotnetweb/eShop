using eShop.WebApplication.DataLayer.EntityContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.BaseServices
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        private IUnitOfWork _unitOfWork;
        private IDbSet<T> _dbSet;
        public EntityService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            _dbSet = _unitOfWork.Set<T>();
        }
        public bool Create(T entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
                return true;
            }
            else
                return false;
        }

        public bool Delete(T entity)
        {
            if (entity != null)
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
                return true;
            }
            else
                return false;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }


        public bool Update(T entity)
        {
            if (entity != null)
            {
                _dbSet.Attach(entity);
                _unitOfWork.Entry(entity).State = EntityState.Modified;
                return true;
            }
            else
                return false;
        }

        public T GetById(int? Id)
        {
            var result = _dbSet.Find(Id);
            if (result != null)
                return result;
            else
                return null;
        }
    }
}
