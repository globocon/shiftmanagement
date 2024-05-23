using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{

    public interface IClientDataProvider
    {        
        public List<Clients> GetClientsList();
        public Clients GetClientsById(int id);
        void DeleteClientDetails(int id);
        public List<Clients> GetClientsListForCompanyAdmin(Guid UserID);

		
		// Task<bool> SaveClientDetailsAsync(int id, string name);
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
            return _context.Clients.Where(x => x.IsDeleted == false).OrderBy(x => x.Name).ToList();
        }

        public Clients GetClientsById(int id)
        {
            return _context.Clients.Where(x => x.IsDeleted == false && x.Id == id).SingleOrDefault();
        }
        //public async Task<bool> SaveClientDetailsAsync(int id, string name)
        //{
        //    try
        //    {
        //        // Retrieve the client from the database
        //        var client = await _context.Clients.FindAsync(id);

        //        if (client != null)
        //        {
        //            // Update the client name
        //            client.Name = name;

        //            // Save changes to the database
        //            await _context.SaveChangesAsync();

        //            return true;
        //        }
        //        else
        //        {
        //            // Client not found
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception

        //        Console.WriteLine($"Error saving client details: {ex.Message}");
        //        return false;
        //    }
        //}
		public int SaveOrUpdateClientDetails(Clients record)
		{
			int saveStatus = 0;
			
            var clientUpdate = _context.Clients.Where(x => x.Id == record.Id && x.Name == record.Name).SingleOrDefault();
			if (clientUpdate == null)
			{
                record.CreationDate = DateTime.Now;
				_context.Add(record);
				saveStatus = 1;
			}
			else
			{
				clientUpdate.Emails = record.Emails;
				clientUpdate.Address = record.Address;
				clientUpdate.Phone = record.Phone;
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
            return _context.Clients.Where(m => _context.USR_Users.Any(a => a.Id == UserID && a.CompanyId == m.CompanyId) && m.IsDeleted == false).OrderBy(x => x.Name).ToList();

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

