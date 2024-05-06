using Newtonsoft.Json.Linq;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Services;

namespace ShiftManagement.Data.Providers
{
	public interface IMissionReportDataProvider
	{
			List<DropDownValues> GetSubmittedByList();
		List<DropDownValues> GetCoTravellerList();
		List<DropDownValues> GetDivisionSectionList();

        List<MissionDetailsHeader> GetDataForViewEditMissionDetails();
		int SaveUpdateMissionReport(MissionDetailsHeader missionDetailsHeader, out string requestNumber);

	}

	public class MissionReportDataProvider : IMissionReportDataProvider
	{
		private readonly ShiftDbContext _context;
		private readonly IExternalDataApiCallService _externaldataapicallservice;

		public MissionReportDataProvider(ShiftDbContext context,
			IExternalDataApiCallService ExternalDataApiCallService)
		{
			_context = context;
			_externaldataapicallservice = ExternalDataApiCallService;

		}

		
		//code added to Submitted By Dropdown start
		public List<DropDownValues> GetSubmittedByList()
		{
			var ddllist = GetDDLlist("mro_submittedby");
			return ddllist;
		}
		//code added to Submitted By Dropdown stop

		public List<DropDownValues> GetCoTravellerList()
		{						
			var ddllist = GetDDLlist("mro_cotraveller");			
			return ddllist;
		}

		public List<DropDownValues> GetDivisionSectionList()
		{
			var ddllist = GetDDLlist("mro_division/section");
			return ddllist;
		}

		private List<DropDownValues> GetDDLlist(string filterText)
		{
			var list = new List<DropDownValues>();
			var MasterTypeID = _context.FieldTypeMaster.SingleOrDefault(mytable => mytable.FieldTypeName.ToLower().Replace(" ", "").Equals(filterText))?.Id;
			if (MasterTypeID != null)
			{
				var ddllist = _context.FieldDetails.Where(z => z.FieldTypeMasterId == MasterTypeID).OrderBy(x => x.FieldName);
				foreach (var r in ddllist)
				{
					list.Add(new DropDownValues { DropDownId = r.Id, DropDownText = r.FieldName });
				}
			}
			return list;
		}

        public List<MissionDetailsHeader> GetDataForViewEditMissionDetails()
        {
            return _context.MRO_MissionHeader.OrderByDescending(x => x.Id).ToList();            
        }

		public int SaveUpdateMissionReport(MissionDetailsHeader missionDetailsHeader, out string requestNumber)
		{
			requestNumber = "";
			int newrequestid = -1;
			var requestUpdate = _context.MRO_MissionHeader.SingleOrDefault(x => x.Id == missionDetailsHeader.Id);
			if (requestUpdate == null)
				try
				{	
					//string newreqnumber = DateTime.Now.ToString("ddMMyyhhmmssf");
					//newRequestHeader.RequestNumber = newreqnumber;
					_context.MRO_MissionHeader.Add(missionDetailsHeader);
					_context.SaveChanges();
					newrequestid = missionDetailsHeader.Id;
					if (newrequestid > 0)
					{
						
						// Add in detail table
						//NewRequestDetail newRequestDetail = new NewRequestDetail();

						//newRequestDetail.CountryID = "DZ";
						//newRequestDetail.CountryName = "Algeria";
						//newRequestDetail.RequestId = newrequestid;
						//_context.MstctsRequestDetail.Add(newRequestDetail);
						//_context.SaveChanges();
						requestNumber = newrequestid.ToString();
					}
				}
				catch (Exception)
				{

					throw;
				}

			else
			{
				//userUpdate.UserName = user.UserName;
				//userUpdate.Password = user.Password;
				//userUpdate.IsDeleted = user.IsDeleted;
				// _context.SaveChanges();
			}

			return newrequestid;

		}
	}
}