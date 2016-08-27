using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ScheduleShowViewModel
    {
        [Display(Name="نام زمانبند")]
        public string TaskName { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? LastRunTime { get; set; }
        public bool? LastRunWasSuccessful { get; set; }
        public bool IsPaused { get; set; }
    }
}
