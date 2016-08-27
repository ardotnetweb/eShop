using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.OtherViewModel
{
    public class ElmahViewModel
    {
        public string ErrorId { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string StatusCode { get; set; }
        public string URL { get; set; }
        public string Script_Name { get; set; }
        public string Remote_Addr { get; set; }
        public string Login_User { get; set; }
        public string Remote_User { get; set; }
        public string Http_Accept { get; set; }
        public string Http_Method { get; set; }
        public bool TakeCareIP { get; set; }
    }
}
