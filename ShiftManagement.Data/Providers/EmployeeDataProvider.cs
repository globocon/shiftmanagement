
using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{
	public interface IEmployeeDataProvider
	{
		List<Employees> GetEmployees();
		
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

	}
}
