using Microsoft.EntityFrameworkCore;
using ShiftManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Providers
{
    public interface IShiftDataProvider
    {
       List<ShiftDetail> GetEmployeeShiftDataForTheWeek(int EmpId, DateTime StartDate, DateTime EndDate);
		List<ShiftDetail> GetAllEmployeeShiftDataForTheWeek(DateTime StartDate, DateTime EndDate);

	}

    public class ShiftDataProvider: IShiftDataProvider
    {
        public readonly ShiftDbContext _context;
        public ShiftDataProvider(ShiftDbContext context)
        {
            _context = context;
        }

        public List<ShiftDetail> GetEmployeeShiftDataForTheWeek(int EmpId, DateTime StartDate, DateTime EndDate)
        {
            var shft = _context.ShiftDetail.Where(x => x.EmployeeId == EmpId
            && x.ShiftDate.Date >= StartDate.Date && x.ShiftDate.Date <= EndDate.Date)
                .Include(e => e.Employees)
                .Include(t => t.ShiftType)
                .Include(c => c.Clients)
                .OrderBy(o=> o.ShiftDate)
                .ToList();
            return shft;
        }
		public List<ShiftDetail> GetAllEmployeeShiftDataForTheWeek(DateTime StartDate, DateTime EndDate)
		{
			var shft = _context.ShiftDetail.Where(x => x.ShiftDate.Date >= StartDate.Date && x.ShiftDate.Date <= EndDate.Date)
				.Include(e => e.Employees)
				.Include(t => t.ShiftType)
				.Include(c => c.Clients)
				.OrderBy(o => o.ShiftDate)
				.ToList();
			return shft;
		}

	}
    
}
