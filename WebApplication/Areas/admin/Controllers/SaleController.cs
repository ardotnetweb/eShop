using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.SMSViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.SessionState;
using WebApplication.BaseClassWebApplication;
using WebApplication.SMSService;

namespace WebApplication.Areas.admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public partial class SaleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleService _saleService;
        private readonly IApplicationUserManager _userManager;
        private readonly IOrderService _orderService;
        public SaleController(IUnitOfWork unitOfWork, ISaleService saleService,
            IApplicationUserManager userManager, IOrderService orderService)
        {
            this._saleService = saleService;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._orderService = orderService;
        }
        public virtual ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "private,protect")]
        public virtual ActionResult ChartSaleSevenDayAgo()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-7);
            var list = _saleService.GetSaleSevenDayAge().GroupBy(x => x.Date.ToString("d")).ToList();
            var listResult = new List<ResultGroupByChartViewModel>();

            for (var day = startDate; day < endDate; day = day.AddDays(1))
            {
                var element = list.Where(x => x.Key.ToString().Contains(day.ToString("d"))).FirstOrDefault();
                if (element != null)
                {
                    var date = ConvertDateChart.ConvertToPerssionDay(day);
                    listResult.Add(new ResultGroupByChartViewModel { Name = date, Price = element.Sum(x => x.Price) });
                }
                else
                {
                    var date = ConvertDateChart.ConvertToPerssionDay(day);
                    listResult.Add(new ResultGroupByChartViewModel { Name = date, Price = 0 });
                }
            }

            //ResultUltimateChartViewModel
            var listName = new string[7];
            var listValue = new object[7];

            for (int i = 0; i < listResult.Count(); i++)
            {
                listName[i] = listResult[i].Name;
                listValue[i] = listResult[i].Price;
            }
            string sumSales = listValue.Sum(x => Convert.ToDouble(x)).ToString();
            string maxSales = listValue.Max(x => Convert.ToDouble(x)).ToString();
            string minSales = listValue.Min(x => Convert.ToDouble(x)).ToString();

            var resultUltimateChartViewModel = new ResultUltimateChartViewModel
            {
                ListName = listName,
                ListValue = listValue,
                SumSales = sumSales,
                MaxSales = maxSales,
                MinSales = minSales
            };

            //پایان محاسبات

            //رسم چارت


            Highcharts chart = new Highcharts("chart");
            chart.InitChart(new Chart { });
            chart.SetCredits(new Credits { Enabled = false });
            chart.SetYAxis(new YAxis { Title = new YAxisTitle { Text = "دامنه تغیرات در قیمت ها", Style = "font:'normal 14px tahoma', color:'green'" } });


            chart.SetTitle(new Title { Text = "نمودار فروش ،دیروز تا یک هفته قبل", Style = "font:'normal 14px tahoma;word-wrap:break-word;'", Margin = 50 });


            //دلیل ایجاد شده فاصله true نکردن 
            //UseHtml
            chart.SetTooltip(new Tooltip
            {
                Formatter = "function() { return ''+ (this.x.split('/')[0] +' / '+this.x.split('/')[1]) +' : '+ this.y +' '+ 'تومان'; }",
                Style = "font:'normal 12px tahoma'",
                Positioner = "function(boxWidth, boxHeight, point) { return {x:(point.plotX + 20),y:(point.plotY)}; }",
                Crosshairs = new Crosshairs(true),
                UseHTML = true

            });



            //HeaderFormat = "<small style=\"textAlign:red\">Hello</small><table>",
            //PointFormat = "<tr><td><b>روز</b>/ &nbsp;&nbsp;</td><td><b>function() { return ''+ (this.x.split('/')[0] +' / '+this.x.split('/')[1]) +' : '+ this.y +' '+ 'تومان'; }</b></td></tr>" +
            //"<tr><td><b>تاریخ</b>/ &nbsp;&nbsp;</td><td><b>8 فروردین ماه</b></td></tr>" +
            //"<tr><td><b>فروش</b>/ &nbsp;&nbsp;</td><td><b>8900 تومان</b></td></tr>",
            //FooterFormat = "</table>",

            chart.SetXAxis(new XAxis
            {
                Categories = resultUltimateChartViewModel.ListName,
                Labels = new XAxisLabels
                {
                    Enabled = false
                }
            })
            .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "دامنه تغیرات در قیمت ها", Style = "font:'normal 14px tahoma', color:'green'" } })
            .SetSeries(new Series
            {
                Data = new Data(resultUltimateChartViewModel.ListValue),
                Name = " "
            });
            string textSubTitle_to_week = string.Format("جمع کل : {0} / بیشترین فروش : {1} / کمترین فروش : {2}", resultUltimateChartViewModel.SumSales, resultUltimateChartViewModel.MaxSales, resultUltimateChartViewModel.MinSales);
            chart.SetSubtitle(new Subtitle { Text = textSubTitle_to_week, Style = "font:'normal 12px tahoma', color:'green'" });
            return PartialView(MVC.admin.Sale.Views._ChartSaleSevenDayAgo, chart);
        }
        [Authorize(Roles = "private")]
        public virtual PartialViewResult DataSale(string term = "", int count = 20, int page = 0)
        {

            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;
            var list = _saleService.GetAllDataTable(term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _saleService.Count : list.Count;
            return PartialView(viewName: MVC.admin.Sale.Views._DataTableSale, model: list);

        }



        [Authorize(Roles = "private")]
        public virtual PartialViewResult SearchSale()
        {
            var saleCountViewModel = new SaleCountViewModel { Confirmed = _saleService.CountConfirmed, Count = _saleService.Count, NotConfirmed = _saleService.CountNotConfirmed };
            return PartialView(viewName: MVC.admin.Sale.Views._SearchSale, model: saleCountViewModel);
        }


        [Authorize(Roles = "private")]
        public virtual ActionResult DetailsSale(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var model = _saleService.GetSaleById(id);
                if (model != null)
                {
                    return View(model);
                }
                else
                    return RedirectToAction(MVC.admin.Sale.ActionNames.Index);
            }
            else
                return RedirectToAction(MVC.admin.Sale.ActionNames.Index);
        }
        [Authorize(Roles = "private")]
        public virtual ActionResult ConfirmSale(int User_Id, int Sale_Id)
        {
            return PartialView(MVC.admin.Sale.Views._ConfirmSale, new ConfirmSaleViewModel { Sale_Id = Sale_Id, Usder_Id = User_Id });
        }

        [Authorize(Roles = "private")]
        public async virtual Task<ActionResult> ResultConfirmSale(ConfirmSaleViewModel model)
        {
            try
            {
                var user = await _userManager.FindUserById(model.Usder_Id);

                string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                string password = WebConfigurationManager.AppSettings["PasswordSMS"];
                string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


                SendSoapClient client = new SendSoapClient();

                string result = client.SendSimpleSMS2(userName, password, user.Phone, NumberSMS, "کاربر گرامی سفارش شما مورد تائید قرار گرفت بعد از تحویل مرسوله به پست کد رهگیری برای شما ارسال می شود", false);

                var sale = _saleService.GetById(model.Sale_Id);
                sale.StatusUltimate = true;

                _saleService.Update(sale);

                if (_unitOfWork.SaveAllChanges() > 0)
                    return Json(new { Result = "تائید سفارش با موفقیت صورت گرفت" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Result = "عدم موفقیت در تائید سفارش" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { Result = "عدم موفقیت در تائید سفارش" }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize(Roles = "private")]
        public virtual ActionResult SendTrackingNumber(int User_Id, int Sale_Id)
        {
            return PartialView(MVC.admin.Sale.Views._SendTrackingNumber, new ConfirmSaleViewModel { Sale_Id = Sale_Id, Usder_Id = User_Id });
        }
        [Authorize(Roles = "private")]
        public virtual async Task<ActionResult> ResultSendTrackingNumber(ConfirmSaleViewModel model)
        {
            try
            {
                var user = await _userManager.FindUserById(model.Usder_Id);

                string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                string password = WebConfigurationManager.AppSettings["PasswordSMS"];
                string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


                SendSoapClient client = new SendSoapClient();

                string result = client.SendSimpleSMS2(userName, password, user.Phone, NumberSMS, string.Format("شرکت فناوران اطلاعات ارم وب / کد رهگیری پستی / {0} ", model.TrackingNumber.ToString()), false);

                var sale = _saleService.GetById(model.Sale_Id);
                sale.StatusSend = true;
                sale.TrackingNumber = model.TrackingNumber.ToString();


                _saleService.Update(sale);


                if (_unitOfWork.SaveAllChanges() > 0)
                    return Json(new { Result = "ارسال و صحیح کردن کد رهگیری با موفقیت صورت گرفت" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Result = "عدم موفقیت در ارسال و صحیح کردن کد رهگیری" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { Result = "عدم موفقیت در تائید سفارش" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize(Roles = "private")]
        [HttpPost]
        public virtual PartialViewResult DeleteSale(int Id)
        {
            var orders = _orderService.GetAllOrderBySaleId(Id);
            _unitOfWork.RemoveRange<Order>(orders);
            _saleService.Delete(_saleService.GetById(Id));
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessDelete), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete), Status = AlertMode.warning });
        }

        [Authorize(Roles = "private,protect")]
        public virtual ContentResult GetCountNotConfirmed()
        {
            return Content(_saleService.CountNotConfirmed.ToString());
        }
    }
}