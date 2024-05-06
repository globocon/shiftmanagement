using Microsoft.AspNetCore.Mvc.Rendering;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Services
{


	public interface IViewDataService
	{
		List<SelectListItem> TypeOfRequest { get; }
        List<SelectListItem> SubProgramme { get; }

        List<SelectListItem> CountryMaster { get; }

        List<SelectListItem> StaffMaster { get; }

		int GetSequenceNextValue { get; }

        int SaveUpdateNewRequest(NewRequestHeader newRequestHeader, out string requestNumber);

        List<NewRequestHeader> GetDataForViewEditRequest { get; }

        NewRequestHeader GetDataForEditResponse(int RequestId);

    }

    public class ViewDataService : IViewDataService
    {

        private readonly ICreateRequestDataProvider _createRequestDataProvider;

        public ViewDataService(ICreateRequestDataProvider createRequestDataProvider)
        {
            _createRequestDataProvider = createRequestDataProvider;
        }

        //code added for TypeOfRequest dropdown start
        public List<SelectListItem> TypeOfRequest
        {
            get
            {
                var typeOfRequest = _createRequestDataProvider.GetTypeOfRequestList();
                var items = new List<SelectListItem>() { new SelectListItem("Select", "", true) };
                foreach (var item in typeOfRequest)
                {
                    var selectListItem = new SelectListItem(item.TypeOfRequestDesc, item.Id.ToString());
                    //var selectListItem1 = item.Name;
                    //var Default = item.IsDefault;
                    //if (Default == true)
                    //{
                    //	selectListItem.Selected = true;
                    //}
                    items.Add(selectListItem);
                }

                return items;
            }
        }
        //code added for TypeOfRequest dropdown stop

        //code added for SubProgramme dropdown start
        public List<SelectListItem> SubProgramme
        {
            get
            {
                var subProgramme = _createRequestDataProvider.GetSubProgrammeList();
                var items = new List<SelectListItem>() { new SelectListItem("Select", "", true) };
                foreach (var item in subProgramme)
                {
                    var selectListItem = new SelectListItem(item.SubProgrammeDesc, item.Id.ToString());
                    items.Add(selectListItem);
                }

                return items;
            }
        }
        //code added for SubProgramme dropdown stop

        //code added for CountryMaster dropdown start
        public List<SelectListItem> CountryMaster
        {
            get
            {
                var countryMaster = new List<Clients>();   //_createRequestDataProvider.GetCountryList();
                var items = new List<SelectListItem>();
                foreach (var item in countryMaster)
                {
                    var selectListItem = new SelectListItem(item.Name, item.Id.ToString());
                    items.Add(selectListItem);
                }

                return items;
            }
        }
        //code added for CountryMaster dropdown stop

        //code added for StaffMaster dropdown start
        public List<SelectListItem> StaffMaster
        {
            get
            {
                var staffMaster = _createRequestDataProvider.GetStaffList();
                var items = new List<SelectListItem>();
                foreach (var item in staffMaster)
                {
                    var selectListItem = new SelectListItem(item.StaffName, item.StaffId.ToString());
                    items.Add(selectListItem);
                }

                return items;
            }
        }
        //code added for StaffMaster dropdown stop

        public int GetSequenceNextValue
        {
            get
            {
                int result = _createRequestDataProvider.GetNextFormSequenceValue();
                return result;
            }

        }

        public int SaveUpdateNewRequest(NewRequestHeader newRequestHeader,out string requestNumber)
        {

            int result = _createRequestDataProvider.SaveUpdateNewRequest(newRequestHeader,out requestNumber);
            return result;

        }


        public List<NewRequestHeader> GetDataForViewEditRequest
        {
            get
            {
                var newRequestHeader = _createRequestDataProvider.GetDataForViewEditRequest().ToList();
                return newRequestHeader;
            }
        }

        public NewRequestHeader GetDataForEditResponse(int RequestId)
        {
            // var newRequestHeader = _createRequestDataProvider.GetDataForViewEditRequest().FirstOrDefault();
            return _createRequestDataProvider.GetDataForViewEditRequest().FirstOrDefault();

        }
    }
}
