
using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{
	public interface IEmployeeDataProvider
	{
		List<Employees> GetEmployees();
        List<Employees> GetEmployeesForCompanyAdmin(Guid UserId);
    }
	public class EmployeeDataProvider: IEmployeeDataProvider
	{
		private readonly ShiftDbContext _context;

		public EmployeeDataProvider(ShiftDbContext context)
		{
			_context = context;
		}

		public List<Employees> GetEmployees()
		{
			var rtn = _context.Employees.OrderBy(e => e.Name).ToList();
			return rtn;
		}
        public List<Employees> GetEmployeesForCompanyAdmin(Guid UserId)
        {            
            var  rtn = _context.Employees.Where(m => _context.USR_Users.Any(a => a.Id == UserId && a.CompanyId == m.CompanyId)).OrderBy(x => x.Name).ToList();
            return rtn;
        }

    }
}
