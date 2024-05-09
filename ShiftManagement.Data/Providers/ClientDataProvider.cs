using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{

    public interface IClientDataProvider
    {
        public List<Clients> GetClientsList();
        public Clients GetClientsById(int id);
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
