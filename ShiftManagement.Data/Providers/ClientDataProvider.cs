using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Data.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ShiftManagement.Data.Providers
{

    public interface IClientDataProvider
    {        
        public List<Clients> GetClientsList();
        public Clients GetClientsById(int id);
	
		void DeleteClientDetails(int id);
        public List<Clients> GetClientsListForCompanyAdmin(Guid UserID);

		
	
		int SaveOrUpdateClientDetails(Clients record);

        bool SaveNewPublicClientRequest(PublicClientEmployeeRequest pcer, out string msg);
		
	}

    public class ClientDataProvider : IClientDataProvider
    {
        private readonly ShiftDbContext _context;    
        public ClientDataProvider(ShiftDbContext context)
        {
            _context = context;

        }

        public List<Clients> GetClientsList()
        {
            return _context.Clients.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
		}

        public Clients GetClientsById(int id)
        {
            return _context.Clients.Where(x => x.IsDeleted == false && x.Id == id).SingleOrDefault();
		}
	
	
	public int SaveOrUpdateClientDetails(Clients record)
		{
			int saveStatus = 0;
			
            var clientUpdate = _context.Clients.Where(x => x.Id == record.Id ).SingleOrDefault();
			if (clientUpdate == null)
			{
                record.CreationDate = DateTime.Now;
				_context.Add(record);
				saveStatus = 1;
			}
			else
			{
				clientUpdate.Salutation = record.Salutation;
				clientUpdate.FirstName = record.FirstName;
				clientUpdate.SecondName = record.SecondName;
				clientUpdate.LastName = record.LastName;
				clientUpdate.DisplayName = record.DisplayName;
				clientUpdate.Gender = record.Gender;
				clientUpdate.DateOfBirth = record.DateOfBirth;
				clientUpdate.Address = record.Address;
				clientUpdate.UnitOrApartmentNo = record.UnitOrApartmentNo;
				clientUpdate.Mobile = record.Mobile;
				clientUpdate.Phone = record.Phone;
				clientUpdate.Email = record.Email;
				clientUpdate.MaritalStatus = record.MaritalStatus;
				clientUpdate.Nationality = record.Nationality;
				clientUpdate.Languages = record.Languages;
				clientUpdate.ClientStatus = record.ClientStatus;
				saveStatus = 2;
			}
			_context.SaveChanges();
			return saveStatus;
		}
        	
	
		public void DeleteClientDetails(int id)
        {
            if (id == -1)
                return;


            var clientToDelete = _context.Clients.Where(x => x.Id == id).SingleOrDefault();
            if (clientToDelete != null)
            {


                clientToDelete.IsDeleted = true;
                clientToDelete.DeletionDate = DateTime.Now;
                _context.SaveChanges();


            }
        }


        public List<Clients> GetClientsListForCompanyAdmin(Guid UserID)
        {
            return _context.Clients.Where(m => _context.USR_Users.Any(a => a.Id == UserID && a.CompanyId == m.CompanyId) && m.IsDeleted == false).OrderBy(x => x.DisplayName).ToList();

            //return _context.Clients.Where(x => x.IsDeleted == false && x.CompanyId.Value  ).OrderBy(x => x.Name).ToList();
        }

        public bool SaveNewPublicClientRequest(PublicClientEmployeeRequest pcer,out string msg)
        {
            bool rtn = false;
            msg = "Unable to create request. Invalid details !!!.";
            if (pcer != null)
            {
                var ex = _context.PublicClientEmployeeRequest.Where(x => x.Name.ToLower() == pcer.Name.ToLower()
                    && x.RequestType == pcer.RequestType && x.CompanyId == pcer.CompanyId && x.Phone == pcer.Phone
                    && x.RequestStatus == "PENDING").FirstOrDefault();

                if(ex != null)
                {
                    msg = "Unable to create request. Another request already exists. !!!";
                    return rtn;
                }

                _context.PublicClientEmployeeRequest.Add(pcer);
                _context.SaveChanges(true);
                msg = $"Request created successfully with id: {pcer.RequestId.ToString()}";
                rtn = true;
            }
            
            return rtn;
        }
    }
}

