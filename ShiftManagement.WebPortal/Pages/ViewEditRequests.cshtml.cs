using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Models;
using ShiftManagement.WebPortal.Services;

namespace ShiftManagement.WebPortal.Pages
{
    public class ViewEditRequestsModel : PageModel
    {

        private readonly IViewDataService _viewDataService;

        public ViewEditRequestsModel(IViewDataService viewDataService)
        {
            _viewDataService = viewDataService;
        }

        public void OnGet()
        {

           
            

        }
        public IActionResult OnGetViewEditRequestsData()
        {

            return new JsonResult(_viewDataService.GetDataForViewEditRequest);

            // Create a JSON object
            //List<MyJsonObject> data = new List<MyJsonObject>();
            //var myData = new MyJsonObject
            //{
            //    id = 1,
            //    requestNumber = "TCT-001-2023",
            //    receivedDate = "01-Oct-2023",
            //    typeOfRequest= "Letter from the Government",
            //    requestFrom= "Ministry",
            //    addressedTo="Address 1",
            //    subProgramme= "subProgramme 01",
            //    staffResponsible="Staff 01",
            //    responded="Yes",
            //    respondedThrough= "RPTC (Regional Programme Technical Cooperation)",
            //    respondedYear= "2023"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 2,
            //    requestNumber = "TCT-002-2023",
            //    receivedDate = "12-Oct-2023",
            //    typeOfRequest = "Letter from RCO (Resident Coordinator's Office)",
            //    requestFrom = "Ministry",
            //    addressedTo = "Address 2",
            //    subProgramme = "subProgramme 02",
            //    staffResponsible = "Staff 02",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 3,
            //    requestNumber = "TCT-003-2023",
            //    receivedDate = "18-Oct-2023",
            //    typeOfRequest = "IG(intergovernmental) Meeting",
            //    requestFrom = "Ministry",
            //    addressedTo = "Address 3",
            //    subProgramme = "subProgramme 03",
            //    staffResponsible = "Staff 04",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 4,
            //    requestNumber = "TCT-004-2023",
            //    receivedDate = "02-Nov-2023",
            //    typeOfRequest = "Report of CB Activities (Capacity Building Activities)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 5,
            //    requestNumber = "TCT-005-2023",
            //    receivedDate = "03-Nov-2023",
            //    typeOfRequest = "Report of CB Activities (Capacity Building Activities)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 6,
            //    requestNumber = "TCT-006-2023",
            //    receivedDate = "02-Nov-2023",
            //    typeOfRequest = "Report of CB Activities (Capacity Building Activities)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 08",
            //    staffResponsible = "Staff 10",
            //    responded = "Yes",
            //    respondedThrough = "UNDA (United Nations Development Assistance)",
            //    respondedYear = "2023"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 7,
            //    requestNumber = "TCT-007-2023",
            //    receivedDate = "02-Nov-2023",
            //    typeOfRequest = "IG(intergovernmental) Meeting",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 8,
            //    requestNumber = "TCT-008-2023",
            //    receivedDate = "06-Oct-2023",
            //    typeOfRequest = "Letter from RCO (Resident Coordinator's Office)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "Yes",
            //    respondedThrough = "XB (Other, with a free-text option)",
            //    respondedYear = "2023"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 9,
            //    requestNumber = "TCT-009-2023",
            //    receivedDate = "03-Nov-2023",
            //    typeOfRequest = "Report of CB Activities (Capacity Building Activities)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 10,
            //    requestNumber = "TCT-010-2023",
            //    receivedDate = "02-Oct-2023",
            //    typeOfRequest = "Report of CB Activities (Capacity Building Activities)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 08",
            //    staffResponsible = "Staff 10",
            //    responded = "Yes",
            //    respondedThrough = "UNDA (United Nations Development Assistance)",
            //    respondedYear = "2023"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 11,
            //    requestNumber = "TCT-011-2023",
            //    receivedDate = "02-Nov-2023",
            //    typeOfRequest = "IG(intergovernmental) Meeting",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "No"
            //};
            //data.Add(myData);

            //myData = new MyJsonObject
            //{
            //    id = 12,
            //    requestNumber = "TCT-012-2023",
            //    receivedDate = "06-Oct-2023",
            //    typeOfRequest = "Letter from RCO (Resident Coordinator's Office)",
            //    requestFrom = "RC",
            //    addressedTo = "Address 1",
            //    subProgramme = "subProgramme 01",
            //    staffResponsible = "Staff 01",
            //    responded = "Yes",
            //    respondedThrough = "XB (Other, with a free-text option)",
            //    respondedYear = "2023"
            //};
            //data.Add(myData);




            //return new JsonResult(data);

        }
    }

    public class MyJsonObject
    {
        public int id { get; set; }
        public string? requestNumber { get; set; }
        public string? receivedDate { get; set; }
        public string? typeOfRequest { get; set; }
        public string? requestFrom { get; set; }
        public string? addressedTo { get; set; }
        public string? subProgramme { get; set; }
        public string? staffResponsible { get; set; }
        public string? responded { get; set; }
        public string? respondedThrough { get; set; }
        public string? respondedYear { get; set; }
    }
}
