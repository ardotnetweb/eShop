using DNTScheduler;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.ImplamentService;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication.SMSService;

namespace WebApplication.WebTask
{
    public class SaleAlert : ScheduledTaskTemplate
    {
        public override int Order
        {
            get { return 2; }
        }

        public override bool RunAt(DateTime now)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;
            return now.Hour % 23 == 0 && now.Second == 1;
        }

        public override void Run()
        {

            if (this.IsShuttingDown || this.Pause)
            {
                return;
            }
            try
            {
                string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                string password = WebConfigurationManager.AppSettings["PasswordSMS"];
                string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];
                string PhoneManager = WebConfigurationManager.AppSettings["PhoneManager"];
                SendSoapClient client = new SendSoapClient();

                var resultNotConfirmed = new CalculatedSale().CountNotConfirmed();

                if (resultNotConfirmed > 20)
                {
                    string resultSMS = client.SendSimpleSMS2(userName, password, PhoneManager, NumberSMS, string.Format("فناوران اطلاعات ارم وب / {0} سفارش در سیستم ثبت شده است.", resultNotConfirmed.ToString()), false);
                }
            }
            catch
            {

            }
        }

        public override string Name
        {
            get { return "مدیرت تائید سفارش های ثبت شده تائید نشده"; }
        }
    }


    public class CalculatedSale
    {
        private readonly ISaleService _saleService;
        public CalculatedSale()
        {
            _saleService = new SaleService(new DataContext());
        }
        public int CountNotConfirmed()
        {
            return _saleService.CountNotConfirmed;
        }
    }
}