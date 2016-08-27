using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.ImplamentService
{
    public class SliderService : EntityService<Slider>, ISliderService
    {
        private readonly IDbSet<Slider> _dbSet;
        private IUnitOfWork _unitOfWork;
        public SliderService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Slider>();
        }

        public List<DeleteSliderViewModel> GetAllImageForDelete()
        {
            return _dbSet.AsQueryable().Select(x => new DeleteSliderViewModel
            {
                Id = x.Id,
                ImageSlider = x.Image,
                Title=x.Title
            }).ToList();
        }
    }
}
