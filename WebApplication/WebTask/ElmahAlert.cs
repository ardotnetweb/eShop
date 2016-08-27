using DNTScheduler;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.ImplamentService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using WebApplication.SMSService;

namespace WebApplication.WebTask
{
    public class ElmahAlert : ScheduledTaskTemplate
    {
        public override int Order
        {
            get { return 1; }
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

                var address = HostingEnvironment.MapPath("~/App_Data/ErrorsLog/");

                int fxmlCount = Directory.GetFiles(address, @"*.xml",
                    SearchOption.TopDirectoryOnly).Length;

                if (fxmlCount > 30)
                {
                    string resultSMS = client.SendSimpleSMS2(userName, password, PhoneManager, NumberSMS, string.Format("فناوران اطلاعات ارم وب / {0} خطا در سیستم ثبت شده است نسبت به رفع آن و حذف خطا اقدام نماید", fxmlCount.ToString()), false);
                }
                else if (fxmlCount > 40)
                {
                    string resultSMS = client.SendSimpleSMS2(userName, password, PhoneManager, NumberSMS, string.Format(" فناوران اطلاعات ارم وب / {0} خطا در سیتم ثبت شده در صورت پاک نکردن خطا ها سیستم با هنگی مواجه می شود", fxmlCount.ToString()), false);
                }
            }
            catch
            {

            }
        }

        public override string Name
        {
            get { return "مدیرت هشدار خطاهای ثبت شده"; }
        }
    }
}