using DNTScheduler;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.ImplamentService;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.WebTask
{
    public class PackageAlert : ScheduledTaskTemplate
    {
        public override int Order
        {
            get { return 3; }
        }

        public override bool RunAt(DateTime now)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;
            return now.Hour % 9 == 0 && now.Second == 1;
        }

        public override void Run()
        {

            if (this.IsShuttingDown || this.Pause)
            {
                return;
            }
            try
            {
                new CalculatePackage().ToDoDisablePackage();
            }
            catch
            {

            }
        }

        public override string Name
        {
            get { return "مدیریت غیر فعال کردن پکیج های تاریخ گذشته"; }
        }
    }

    public class CalculatePackage
    {
        private readonly IPackageService _packageService;
        public CalculatePackage()
        {
            _packageService = new PackageService(new DataContext());
        }
        public void ToDoDisablePackage()
        {
            _packageService.ToDoDisablePackage();
        }
    }
}