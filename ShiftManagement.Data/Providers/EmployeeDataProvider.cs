
using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{
    public interface IEmployeeDataProvider
    {
        List<Employees> GetEmployees();
        List<Employees> GetEmployeesForCompanyAdmin(Guid UserId);

        public Employees GetEmployeesById(int id);
        void DeleteEmployeeDetails(int id);

        int SaveOrUpdateEmployeeDetails(Employees record);
    }

    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        private readonly ShiftDbContext _context;

        public EmployeeDataProvider(ShiftDbContext context)
        {
            _context = context;
        }

        public List<Employees> GetEmployees()
        {
            var rtn = _context.Employees.Where(x => x.IsDeleted == false).OrderBy(e => e.Name).ToList();
            return rtn;
        }

        public List<Employees> GetEmployeesForCompanyAdmin(Guid UserId)
        {
            var rtn = _context.Employees.Where(m => _context.USR_Users.Any(a => a.Id == UserId && a.CompanyId == m.CompanyId)).OrderBy(x => x.Name).ToList();
            return rtn;
        }

        public Employees GetEmployeesById(int id)
        {
            return _context.Employees.Where(x => x.IsDeleted == false && x.Id == id).SingleOrDefault();
        }
        public int SaveOrUpdateEmployeeDetails(Employees record)
        {
            int saveStatus = 0;

            var EmployeeUpdate = _context.Employees.Where(x => x.Id == record.Id).SingleOrDefault();
            if (EmployeeUpdate == null)
            {
                record.CreatedDateTime = DateTime.Now;
                _context.Add(record);
                saveStatus = 1;
            }
            else
            {
                EmployeeUpdate.Name = record.Name;
                EmployeeUpdate.Phone = record.Phone;
                EmployeeUpdate.DOB = record.DOB;
                EmployeeUpdate.DOJ = record.DOJ;
                saveStatus = 2;
            }
            _context.SaveChanges();
            return saveStatus;
        }


        public void DeleteEmployeeDetails(int id)
        {
            if (id == -1)
                return;


            var clientToDelete = _context.Employees.Where(x => x.Id == id).SingleOrDefault();
            if (clientToDelete != null)
            {


                clientToDelete.IsDeleted = true;
                _context.SaveChanges();


            }
        }
    }

}
