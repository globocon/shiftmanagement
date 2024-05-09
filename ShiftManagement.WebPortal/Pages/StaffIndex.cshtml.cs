using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Data;
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;
using System.Security.Claims;

namespace ShiftManagement.WebPortal.Pages
{
    public class StaffIndexModel : PageModel
    {
       
        private readonly ILogger<StaffIndexModel> _logger;
        private readonly IShiftDataProvider _shiftDataProvider;
       
        public StaffIndexModel(ILogger<StaffIndexModel> logger, IShiftDataProvider shiftDataProvider)
        {
            _logger = logger;
            _shiftDataProvider = shiftDataProvider;
        }

        public void OnGet()
        {

        }
        public IActionResult OnGetEmployeeShiftData(DateTime? shiftDate)
        {
            List<ShiftDetail> ShiftData = new List<ShiftDetail>();
            var startDate = DateHelper.thisWeekStart;
            var endDate = DateHelper.thisWeekEnd;

            if(shiftDate != null)
            {
                DateTime ndtm = new DateTime(shiftDate.Value.Year, shiftDate.Value.Month, shiftDate.Value.Day);
                startDate = DateHelper.thisWeekStartOfDate(ndtm);
                endDate = DateHelper.thisWeekEndOfDate(ndtm);
            }            

            var empid = User.Claims.FirstOrDefault(x => x.Type == "Eid").Value;
            if(empid != string.Empty)
                ShiftData = _shiftDataProvider.GetEmployeeShiftDataForTheWeek(Convert.ToInt32(empid), startDate, endDate);

            return new JsonResult(new { ShiftData, startDate = startDate.ToString("dd/MM/yyyy"), endDate = endDate.ToString("dd/MM/yyyy") });
        }
               
       
    }
}
    