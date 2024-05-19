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
        bool SaveNewShift(ShiftDetail sd, out string msg);
        bool UpdateShift(ShiftDetail sd, out string msg);
        bool DeleteShift(int sdId, out string msg);
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
                .OrderBy(o => o.ShiftDate)
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

        public bool SaveNewShift(ShiftDetail sd, out string msg)
        {
            bool issuccess = false;
            msg = "";

            if (sd != null)
            {
                var chkifexists = _context.ShiftDetail.Where(x => x.ShiftDate == sd.ShiftDate && x.EmployeeId == sd.EmployeeId).FirstOrDefault();
                if (chkifexists == null)
                {
                    sd.CalanderId = -1;
                    _context.ShiftDetail.Add(sd);
                    _context.SaveChanges();
                    sd.CalanderId = sd.Id;
                    _context.SaveChanges();
                    issuccess = true;
                    msg = "Shift detail saved successfully.";
                }
                else
                {
                    msg = "Shift detail already exists for the employee with same date.";
                }
            }
            else
            {
                msg = "Invalid Shift details.";
            }

            return issuccess;
        }

        public bool UpdateShift(ShiftDetail sd, out string msg)
        {
            bool issuccess = false;
            msg = "";

            if (sd != null)
            {
                var chkifexists = _context.ShiftDetail.Where(x => x.Id == sd.Id).FirstOrDefault();
                if (chkifexists != null)
                {
                    chkifexists.ShiftTypeID = sd.ShiftTypeID;
                    chkifexists.ClientId = sd.ClientId;
                    chkifexists.StartTime = sd.StartTime;
                    chkifexists.EndTime = sd.EndTime;
                    _context.ShiftDetail.Update(chkifexists);
                    _context.SaveChanges();
                    issuccess = true;
                    msg = "Shift detail saved successfully.";
                }
                else
                {
                    msg = "Shift detail not found.";
                }
            }
            else
            {
                msg = "Invalid Shift details.";
            }

            return issuccess;
        }

        public bool DeleteShift(int sdId, out string msg)
        {
            bool issuccess = false;
            msg = "";

            var chkifexists = _context.ShiftDetail.Where(x => x.Id == sdId).SingleOrDefault();
            if (chkifexists != null)
            {
                _context.ShiftDetail.Remove(chkifexists);
                _context.SaveChanges();
                issuccess = true;
                msg = "Shift detail deleted successfully.";
            }
            else
            {
                msg = "Shift detail not found.";
            }

            return issuccess;
        }    

    }

}
