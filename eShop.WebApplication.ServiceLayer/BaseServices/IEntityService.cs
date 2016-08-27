
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.BaseServices
{
    public interface IEntityService<T> : IService where T : class
    {
        bool Create(T entity);
        bool Delete(T entity);
        List<T> GetAll();
        bool Update(T entity);
        T GetById(int? Id);

    }
}
