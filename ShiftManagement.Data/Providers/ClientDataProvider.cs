using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{

    public interface IClientDataProvider
    {
        public List<Clients> GetClientsList();
        public Clients GetClientsById(int id);
        int AddorUpdateSiteDetails(SiteMaster site);
        void DeleteClientDetails(int id);
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
            return _context.Clients.Where(x=> x.IsDeleted == false).OrderBy(x => x.Name).ToList();
        }

		public Clients GetClientsById(int id)
        {
			return _context.Clients.Where(x => x.IsDeleted == false && x.Id == id).SingleOrDefault();
		}


		public int AddorUpdateSiteDetails(SiteMaster site)
        {
            int saveStatus = 0;
            if (site != null)
            {

                if (site.Id == -1)
                {
                    /* for checking already exist this title  */
                    var checkIfAlreadyExist = _context.SiteMaster.FirstOrDefault(x => x.SiteName == site.SiteName);
                    if (checkIfAlreadyExist == null)
                    {
                        _context.SiteMaster.Add(new SiteMaster()
                        {
                            SiteName = site.SiteName,
                            SiteAddress = site.SiteAddress

                        });
                        saveStatus = 1;
                    }
                    else
                    {
                        saveStatus = 2;
                    }

                }
                else
                {
                    var reportFieldToUpdate = _context.SiteMaster.SingleOrDefault(x => x.Id == site.Id);

                    if (reportFieldToUpdate != null)
                    {
                        /* for checking already exist this title in state */
                        var checkIfAlreadyExist = _context.SiteMaster.FirstOrDefault(x => x.SiteName == site.SiteName && x.Id != site.Id);
                        if (checkIfAlreadyExist == null)
                        {
                            reportFieldToUpdate.SiteName = site.SiteName;
                            reportFieldToUpdate.SiteAddress = site.SiteAddress;
                            saveStatus = 1;
                        }
                        else
                        {

                            saveStatus = 3;
                        }
                    }
                }
                _context.SaveChanges();

            }

            return saveStatus;
        }
        
        public void DeleteClientDetails(int id)
        {
            if (id == -1)
                return;

            var ClientsDetailsToDelete = _context.Clients.SingleOrDefault(x => x.Id == id);
            if (ClientsDetailsToDelete == null)
                throw new InvalidOperationException();

            ClientsDetailsToDelete.Status = 0;
            ClientsDetailsToDelete.IsDeleted = true;

			_context.SaveChanges();
        }
    }
}
