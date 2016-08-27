using DNTScheduler;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.OtherViewModel;
using eShop.WebApplication.DomainClasses.SMSViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml.Linq;
using WebApplication.SMSService;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    [SessionState(SessionStateBehavior.Disabled)]
    public partial class SettingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IElmahService _elmahService;
        private IBlockService _blockService;
        public SettingController(IUnitOfWork unitOfWork,
                                 IElmahService elmahService,
                                 IBlockService blockService)
        {
            this._unitOfWork = unitOfWork;
            this._elmahService = elmahService;
            this._blockService = blockService;
        }
        public virtual ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public virtual ActionResult Index(List<string> checkboxImage)
        {
            return View();
        }


        [HttpGet]
        public virtual PartialViewResult ReadElmahException()
        {
            int fxmlCount = Directory.GetFiles(Server.MapPath("~/App_Data/ErrorsLog/"), @"*.xml",
                SearchOption.TopDirectoryOnly).Length;
            if (fxmlCount > 0)
            {
                string[] files = new string[fxmlCount];
                files = Directory.GetFiles(Server.MapPath("~/App_Data/ErrorsLog/"), @"*.xml",
                        SearchOption.TopDirectoryOnly);
                var listElamViewModel = new List<ElmahViewModel>();
                var elmahViewModel = new ElmahViewModel();

                foreach (var item in files)
                {
                    XElement root = XElement.Load(Server.MapPath("~/App_Data/ErrorsLog/" +
                                   Path.GetFileName(item)));

                    elmahViewModel.ErrorId = root.Attribute("errorId") != null ? root.Attribute("errorId").Value : "--";
                    elmahViewModel.ExceptionType = root.Attribute("type") != null ? root.Attribute("type").Value : "--";
                    elmahViewModel.Message = root.Attribute("message") != null ? root.Attribute("message").Value : "--";


                    if (root.Attribute("time").Value != null)
                    {
                        var dateTime = Convert.ToDateTime(root.Attribute("time").Value);
                        elmahViewModel.Date = dateTime;
                        elmahViewModel.Time = dateTime.ToString("hh:mm:ss");
                    }
                    else
                    {
                        elmahViewModel.Date = DateTime.Parse("1/1/2001");
                        elmahViewModel.Time = null;
                    }

                    if (root.Attribute("statusCode") != null)
                        elmahViewModel.StatusCode = root.Attribute("statusCode").Value ?? "--";


                    var serverVariable = root.Element("serverVariables").Descendants("item");




                    elmahViewModel.Script_Name = serverVariable.Where(x => (string)x.Attribute("name") == "SCRIPT_NAME")
                        .FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value ?? "--";

                    elmahViewModel.URL = serverVariable.Where(x => (string)x.Attribute("name") == "URL")
                        .FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value;

                    elmahViewModel.Remote_Addr = serverVariable.Where(x => (string)x.Attribute("name") == "REMOTE_ADDR")
                        .FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value ?? "--";

                    elmahViewModel.Login_User = serverVariable.Where(x => (string)x.Attribute("name") == "LOGON_USER").
                        FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value;

                    elmahViewModel.Remote_User = serverVariable.Where(x => (string)x.Attribute("name") == "REMOTE_USER").
                        FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value ?? "--";


                    elmahViewModel.Http_Accept = serverVariable.Where(x => (string)x.Attribute("name") == "HTTP_ACCEPT")
                        .FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value ?? "--";


                    elmahViewModel.Http_Method = serverVariable.Where(x => (string)x.Attribute("name") == "REQUEST_METHOD")
                        .FirstOrDefault().Descendants("value").Attributes("string").FirstOrDefault().Value ?? "--";




                    listElamViewModel.Add(elmahViewModel);
                    elmahViewModel = new ElmahViewModel();
                }

                return PartialView(viewName: MVC.admin.Setting.Views._DataElmah,
                    model: listElamViewModel);
            }
            else
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.NoItem),
                    Status = AlertMode.White
                });
            }
        }



        public virtual PartialViewResult ComputeAndDeleteElmah
            ([Bind(Include = "Date,ErrorId,Message,Time,Remote_Addr,Http_Method,TakeCareIP",
                Exclude = "ExceptionType,StatusCode,URL,Script_Name,Login_User,Remote_User,Http_Accept")]
            IDictionary<string, ElmahViewModel> dictinaryViewModels)
        {
            try
            {
                var namesFiles = System.IO.Directory.GetFiles(Server.MapPath("~/App_Data/ErrorsLog/"),
                                 "*.xml", SearchOption.TopDirectoryOnly);
                var listElmah = new List<ElmahModel>();
                foreach (var item in dictinaryViewModels)
                {
                    if (item.Value.TakeCareIP)
                    {
                        listElmah.Add(new ElmahModel
                        {
                            Date = item.Value.Date,
                            HttpMethod = item.Value.Http_Method,
                            IP = item.Value.Remote_Addr,
                            Message = item.Value.Message,
                            Time = TimeSpan.Parse(item.Value.Time)
                        });
                    }
                    string name = namesFiles.Where(x => x.Contains(item.Value.ErrorId)).
                        Select(x => x).FirstOrDefault();

                    if (System.IO.File.Exists(Server.MapPath("~/App_Data/ErrorsLog/" + Path.GetFileName(name))))
                        System.IO.File.Delete(Server.MapPath("~/App_Data/ErrorsLog/" + Path.GetFileName(name)));

                }
                if (listElmah.Count() > 0)
                {
                    _unitOfWork.AddRange<ElmahModel>(listElmah);
                    if (_unitOfWork.SaveAllChanges() > 0)
                    {
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperation.SurveyOperation(StatusOperation.DeleteInsertElmah),
                            Status = AlertMode.success
                        });
                    }
                    else
                    {
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete),
                            Status = AlertMode.warning
                        });
                    }
                }
                else
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                    {
                        Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessDelete),
                        Status = AlertMode.success
                    });
            }
            catch
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete),
                    Status = AlertMode.warning
                });
            }
        }



        public virtual ActionResult IndexIPDanger()
        {
            return View();
        }

        public virtual ActionResult DataIPDanger(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _elmahService.GetAllPaging(term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _elmahService.Count : list.Count;

            return PartialView(viewName: MVC.admin.Setting.Views._DataIPDanger, model: list);
        }
        public virtual PartialViewResult SerachIPDanger()
        {
            return PartialView(viewName: MVC.admin.Setting.Views._SearchIPDanger,
                model: _elmahService.Count);
        }


        public virtual JsonResult DeleteIPDanger(int Id)
        {
            _elmahService.Delete(_elmahService.GetById(Id));
            if (_unitOfWork.SaveAllChanges() > 0)
                return Json(new { Status = "true" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Status = "false" }, JsonRequestBehavior.AllowGet);
        }


        public virtual ActionResult DetailsIPDanger(int Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_elmahService.IsExist(id))
                {
                    var model = _elmahService.GetById(id);
                    return View(model: _elmahService.GetAllByIP(model.IP));
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Setting.ActionNames.IndexIPDanger);

            }
            else
                return RedirectToAction(actionName: MVC.admin.Setting.ActionNames.IndexIPDanger);
        }


        [HttpPost]
        public virtual ActionResult DeleteIPDanger(string IPAddress)
        {
            var list = _elmahService.GetAllByIP(IPAddress);
            _unitOfWork.RemoveRange<ElmahModel>(list);
            _blockService.Create(new BlockModel { Date = DateTime.Now, IP = IPAddress });
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert),
                    Status = AlertMode.success
                });
            }
            else
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert),
                    Status = AlertMode.warning
                });
            }
        }





        public virtual ActionResult ServiceSMS()
        {
            return View();
        }


        public virtual ActionResult getInfoAccount()
        {
            try
            {
                InfoAccountSMS info = new InfoAccountSMS();
                var userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                var password = WebConfigurationManager.AppSettings["PasswordSMS"];


                SendSoapClient client = new SendSoapClient();

                int countUnReed = client.GetInboxCount(userName, password, false);
                int countReed = client.GetInboxCount(userName, password, true);

                var creadit = Math.Round(client.GetCredit(userName, password), 2).ToString();
                var countRecive = (countUnReed + countReed).ToString();
                var countSend = "120";

                info.CountRead = countReed.ToString();
                info.CountUnRead = countUnReed.ToString();
                info.CountSend = countSend;
                info.CountRecive = countRecive;
                info.Creadit = creadit;

                return PartialView(viewName: MVC.admin.Setting.Views._DetailsAccountSMS,
                    model: info);

            }
            catch (Exception ex)
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.ExceptionReciveSMS),
                    Status = AlertMode.warning
                });
            }
        }




        [HttpPost]
        public virtual ActionResult SendSimpleSMS2(SndSMSViewModel model)
        {
            try
            {
                string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                string password = WebConfigurationManager.AppSettings["PasswordSMS"];
                string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


                SendSoapClient client = new SendSoapClient();

                string result = client.SendSimpleSMS2(userName, password, model.Phone, NumberSMS, model.Body, false);

                switch (result)
                {
                    case "0":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.InvalidUserNameOrPassword),
                            Status = AlertMode.warning
                        });
                    case "1":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.SuccessOperation),
                            Status = AlertMode.success
                        });
                    case "2":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.NotEnoghCreadit),
                            Status = AlertMode.warning
                        });
                    case "3":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.DailySendLimit),
                            Status = AlertMode.warning
                        });
                    case "4":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.WightSendLimit),
                            Status = AlertMode.warning
                        });
                    case "5":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.NotValidNumber),
                            Status = AlertMode.warning
                        });
                    case "6":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.UpgradeSoftwer),
                            Status = AlertMode.warning
                        });
                    case "7":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.FilterWord),
                            Status = AlertMode.warning
                        });
                    case "10":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.NotActiveUser),
                            Status = AlertMode.warning
                        });
                    case "11":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.NotSend),
                            Status = AlertMode.warning
                        });
                    case "12":
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.NotCompleteRegistrationPaper),
                            Status = AlertMode.warning
                        });
                    default:
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                        {
                            Alert = AlertOperationSendSMS.SurveyOperation(StatusOperationSendMessage.RecId),
                            Status = AlertMode.success
                        });
                }
            }
            catch
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailSend),
                    Status = AlertMode.warning
                });
            }
        }



        public virtual ActionResult GetUnReadSMS()
        {

            var result = new List<InfoSMSViewModel> { };

            string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
            string password = WebConfigurationManager.AppSettings["PasswordSMS"];
            string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


            SendSoapClient client = new SendSoapClient();
            var countUnRead = client.GetInboxCount(userName, password, false);

            if (countUnRead != 0)
            {
                if (countUnRead <= 5)
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, 0, countUnRead);
                    result = resultSMS.Select(x => new InfoSMSViewModel
                     {
                         Id = x.MsgID.ToString(),
                         Body = x.Body,
                         DateTime = x.SendDate,
                         Sender = x.Sender
                     }).ToList();
                    return View(result);
                }
                else
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, 0, 5);
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return View(result);
                }
            }
            else
                return View(new List<InfoSMSViewModel> { });

        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult dataSMSInfo(int? page)
        {

            var result = new List<InfoSMSViewModel> { };

            var pageNumber = page ?? 0;
            string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
            string password = WebConfigurationManager.AppSettings["PasswordSMS"];
            string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


            SendSoapClient client = new SendSoapClient();
            var countUnRead = client.GetInboxCount(userName, password, false);
            if (countUnRead != 0)
            {
                if (countUnRead <= 30)
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, (pageNumber * 30), countUnRead);
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return PartialView(viewName: MVC.admin.Setting.Views._dataSMS, model: result);
                }
                else
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, (pageNumber * 30), 30);
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return PartialView(viewName: MVC.admin.Setting.Views._dataSMS, model: result);
                }
            }
            else
                return Content("no-more-info");
        }



        public virtual ActionResult GetReadSMS()
        {

            var result = new List<InfoSMSViewModel> { };

            string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
            string password = WebConfigurationManager.AppSettings["PasswordSMS"];
            string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


            SendSoapClient client = new SendSoapClient();
            var countUnRead = client.GetInboxCount(userName, password, false);
            var counRead = client.GetInboxCount(userName, password, true);
            if (counRead != 0)
            {
                if (countUnRead != 0)
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, (0 + countUnRead), 30);
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return View(result);
                }
                else
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, 0, 30);
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return View(result);
                }
            }
            else
                return View(new List<InfoSMSViewModel> { });
        }


        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult dataSMSInfoRead(int? page)
        {

            var pageNumber = page ?? 0;
            var result = new List<InfoSMSViewModel> { };

            string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
            string password = WebConfigurationManager.AppSettings["PasswordSMS"];
            string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];

            SendSoapClient client = new SendSoapClient();
            var countUnRead = client.GetInboxCount(userName, password, false);
            var counRead = client.GetInboxCount(userName, password, true);
            if (counRead != 0)
            {
                if (countUnRead != 0)
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, ((pageNumber * 30) + countUnRead), 30);
                    if (resultSMS == null || !resultSMS.Any())
                        return Content("no-more-info");
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return PartialView(viewName: MVC.admin.Setting.Views._dataSMS, model: result);
                }
                else
                {
                    var resultSMS = client.getMessages(userName, password, 1, null, (pageNumber * 30), 30);
                    if (resultSMS == null || !resultSMS.Any())
                        return Content("no-more-info");
                    result = resultSMS.Select(x => new InfoSMSViewModel
                    {
                        Id = x.MsgID.ToString(),
                        Body = x.Body,
                        DateTime = x.SendDate,
                        Sender = x.Sender
                    }).ToList();
                    return PartialView(viewName: MVC.admin.Setting.Views._dataSMS, model: result);
                }
            }
            else
                return Content("no-more-info");
        }


        public virtual ActionResult SchedulerList()
        {
            var jobsList = ScheduledTasksCoordinator.Current.ScheduledTasks.Select(x => new ScheduleShowViewModel
                {
                    TaskName = x.Name,
                    LastRunTime = x.LastRun,
                    LastRunWasSuccessful = x.IsLastRunSuccessful,
                    IsPaused = x.Pause,
                }).ToList();
            return View(jobsList);
        }



        public virtual ActionResult GetIPForAutoComplete(string term)
        {
            var list = _elmahService.GetIPForAutoComplete(term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}