using iText.Commons.Actions.Contexts;
using iText.Layout.Element;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Services;

namespace ShiftManagement.Data.Providers
{

	public	interface ICreateRequestDataProvider
	{

		List<TypeOfRequest> GetTypeOfRequestList();
        List<SubProgramme> GetSubProgrammeList();
        List<StaffMaster> GetStaffList();
        int GetNextFormSequenceValue();
        int SaveUpdateNewRequest(NewRequestHeader newRequestHeader, out string requestNumber);

        List<NewRequestHeader> GetDataForViewEditRequest();

        NewRequestHeader GetDataForEditResponse(int RequestId);

    }
	public class CreateRequestDataProvider: ICreateRequestDataProvider
	{
		private readonly ShiftDbContext _context;
        private readonly IExternalDataApiCallService _externaldataapicallservice;

        public  CreateRequestDataProvider(ShiftDbContext context, 
            IExternalDataApiCallService ExternalDataApiCallService)
		{
			_context = context;
            _externaldataapicallservice = ExternalDataApiCallService;

        }

        //code added to TypeOfRequest Dropdown start
        public List<TypeOfRequest> GetTypeOfRequestList()
		{
			var list = new List<TypeOfRequest>();
			var typeOfReq = _context.FieldDetails.Where(z=> z.FieldTypeMasterId == 3).OrderBy(x => x.FieldName);
			foreach (var r in typeOfReq)
			{
				list.Add(new TypeOfRequest { Id = r.Id , TypeOfRequestDesc = r.FieldName}  );
			}
			return list;
		}
        //code added to TypeOfRequest Dropdown stop

        //code added to SubProgramme Dropdown start
        public List<SubProgramme> GetSubProgrammeList()
        {
            var list = new List<SubProgramme>();
            var subProgramme = _context.FieldDetails.Where(z => z.FieldTypeMasterId == 4).OrderBy(x => x.FieldName);
            foreach (var r in subProgramme)
            {
                list.Add(new SubProgramme { Id = r.Id, SubProgrammeDesc = r.FieldName });
            }
            return list;
        }
        //code added to SubProgramme Dropdown stop

        
        //code added to StaffMaster Dropdown start
        public List<StaffMaster> GetStaffList()
        {
            var list = new List<StaffMaster>();
            var staffMaster = _externaldataapicallservice.GetStaffMasterFromApi();
            list = JsonConvert.DeserializeObject<List<StaffMaster>>(staffMaster);
            return list;
        }
		//code added to StaffMaster Dropdown stop


		//code added for getting sequence value
		public int GetNextFormSequenceValue()
		{
			int result = 0;			
			//var db = _context.Database; //  .db(); //change the name to whatever your LINQ names are            
			//int nextId = db.SqlQueryRaw<int>("SELECT NEXT VALUE FOR [SeqNewRequest]").FirstOrDefault();


            var p = new Microsoft.Data.SqlClient.SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction  = System.Data.ParameterDirection.Output;
            _context.Database.ExecuteSqlRaw("set @result = next value for SeqNewRequest", p);
            var nextId = (int)p.Value;



            if (nextId > 0)
			{
				result = nextId;
			}
			return result;
		}

        public int SaveUpdateNewRequest(NewRequestHeader newRequestHeader,out string requestNumber)
        {
            requestNumber = "";
            int newrequestid = -1;
            var requestUpdate = _context.MstctsRequestHeader.SingleOrDefault(x => x.RequestId == newRequestHeader.RequestId);
            if (requestUpdate == null)
                try
                {
                    //List<NewRequestDetail> newRequestDetailList = new List<NewRequestDetail>();
                    //NewRequestDetail newRequestDetail = new NewRequestDetail();

                    //newRequestDetail.CountryID = "DZ";
                    //newRequestDetail.CountryName = "Algeria";
                    // newRequestDetail.RequestId = newRequestHeader.RequestId;

                    //newRequestDetailList.Add(newRequestDetail);
                    //newRequestHeader.NewRequestDetail.Add(newRequestDetail);

                    string newreqnumber =  DateTime.Now.ToString("ddMMyyhhmmssf");
                    newRequestHeader.RequestNumber = newreqnumber;
                   _context.MstctsRequestHeader.Add(newRequestHeader);
                    _context.SaveChanges();
                    newrequestid = newRequestHeader.RequestId;
                    if (newrequestid > 0)
                    {
                        string newseq = GetNextFormSequenceValue().ToString("000");
                        newreqnumber = "TCT-" + newseq + "-" + DateTime.Today.Year.ToString();
                        newRequestHeader.RequestNumber = newreqnumber;
                        _context.MstctsRequestHeader.Update(newRequestHeader);
                        _context.SaveChanges();

                        // Add in detail table
                        NewRequestDetail newRequestDetail = new NewRequestDetail();

                        newRequestDetail.CountryID = "DZ";
                        newRequestDetail.CountryName = "Algeria";
                        newRequestDetail.RequestId = newrequestid;
                        _context.MstctsRequestDetail.Add(newRequestDetail);
                        _context.SaveChanges();
                        requestNumber = newreqnumber;
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


        public List<NewRequestHeader> GetDataForViewEditRequest()
        {

            return _context.MstctsRequestHeader.OrderByDescending(x => x.RequestNumber).ToList();
            //var list = new List<NewRequestHeader>();
            //var requestList = _context.MstctsRequestHeader.s  FieldDetails.Where(z => z.FieldTypeMasterId == 3).OrderBy(x => x.FieldName);
            //foreach (var r in typeOfReq)
            //{
            //    list.Add(new TypeOfRequest { Id = r.Id, TypeOfRequestDesc = r.FieldName });
            //}
            //return list;
        }

        public NewRequestHeader GetDataForEditResponse(int RequestId)
        {
            return _context.MstctsRequestHeader.Where(x => x.RequestId == RequestId).FirstOrDefault();
            //var newRequestHeader = _context.MstctsRequestHeader.Where(x => x.RequestId == RequestId);
            //return newRequestHeader;

        }


    }
}
