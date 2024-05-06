using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Data;
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Pages
{
    public class StaffIndexModel : PageModel
    {
        private readonly IModuleDataProvider _moduleData;
        private readonly ILogger<StaffIndexModel> _logger;
        private readonly ShiftDbContext _context;
        public List<int> DistinctYears { get; set; }

        public StaffIndexModel(ILogger<StaffIndexModel> logger, ShiftDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }
        public IActionResult OnGetLast5RequestsData()
        {

            var RequestDetail = _context.MstctsRequestHeader
   .OrderByDescending(x => x.RequestId).Select(n => new { n.RequestId, n.RequestNumber, n.ReceivedOnDate, n.TypeOfReqId, n.RequestFrom, n.RequestTo, n.SubProgrammeId, n.StaffResponsible })
   .Take(5)
   .ToList();

            return new JsonResult(RequestDetail);
        }

        public IActionResult OnGetLast5ResponseData()
        {

            var ResponseDetail = _context.MstctsRequestHeader
   .OrderByDescending(x => x.RequestId).Select(n => new { n.RequestId,n.ReceivedOnDate, n.TypeOfReqId,  n.RequestFrom, n.RespondedThroughId, n.RespondedYear })
   .Take(5)
   .ToList();

            return new JsonResult(ResponseDetail);
        }
        public JsonResult OnGetLatestYear()
        {
            //var value = string.Empty;
            try
            {

               

                DistinctYears = _context.MstctsRequestHeader.Where(p => p.ReceivedOnDate != null && p.ReceivedOnDate.HasValue).Select(p => p.ReceivedOnDate.Value.Year)
               .Distinct()
               .OrderByDescending(year => year)
               .ToList();
               
                return new JsonResult(new { value = DistinctYears });

            }
            catch (Exception ex)
            {
                return new JsonResult("2024");

            }

        }
        public JsonResult OnGetGroupByYears()
        {
       
            try
            {
              
                var yearCounts = _context.MstctsRequestHeader
                .Where(p => p.ReceivedOnDate != null && p.ReceivedOnDate.HasValue)
                .GroupBy(p => p.ReceivedOnDate.Value.Year)
                .Select(g => new { Year = g.Key, Count = g.Count(), RespondCount = g.Count(x => x.Responded == true) })
                .OrderByDescending(year => year.Year)
                .ToList();
         
                return new JsonResult(new { value = yearCounts });

            }
            catch (Exception ex)
            {
                return new JsonResult("2024");

            }




        }
    }
}
    