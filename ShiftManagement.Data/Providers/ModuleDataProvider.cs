using ShiftManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ShiftManagement.Data.Providers
{

    public interface IModuleDataProvider
    {

        List<ModuleOne> GetModuleOneDetails();
        List<ModuleTwo> GetModuleTwoDetails();
        List<MissionDetailsHeader> GetMissionDetails();
        void SaveMissionDetails(MissionDetailsHeader missionDetails);
        void SaveModuleOneDetails(ModuleOne moduleOne);
        void DeleteModuleOneDetails(int id);
        void DeleteMMissionDetails(int id);
        int getyearasint();
      //void GetLast5Requests();
       

    }

    public class ModuleDataProvider : IModuleDataProvider
    {
        private readonly ShiftDbContext _context;
        public ModuleDataProvider(ShiftDbContext context)
        {
            _context = context;

        }
        /// <summary>
        /// Get Module One deatils
        /// </summary>
        /// <returns></returns>
        public List<ModuleOne> GetModuleOneDetails()
        {
            return _context.Tbl_ModuleOne.OrderBy(x => x.Id).ToList();
        }
        /// <summary>
        /// Get Module two deatils
        /// </summary>
        /// <returns></returns>
        public List<ModuleTwo> GetModuleTwoDetails()
        {
            return _context.Tbl_ModuleTwo.OrderBy(x => x.Id).ToList();
        }
        /// <summary>
        /// Get Mission deatils
        /// </summary>
        /// <returns></returns>
        public List<MissionDetailsHeader> GetMissionDetails()
        {
            return _context.MRO_MissionHeader.OrderBy(x => x.Id).ToList();
        }
        /// <summary>
        /// Save or Update moduleOne details
        /// </summary>
        /// <param name="moduleOne"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SaveModuleOneDetails(ModuleOne moduleOne)
        {
            if (moduleOne == null)
                throw new ArgumentNullException();

            if (moduleOne.Id == 0)
            {
                _context.Tbl_ModuleOne.Add(moduleOne);
            }
            else
            {
                var moduleOneDataToUpdate = _context.Tbl_ModuleOne.SingleOrDefault(x => x.Id == moduleOne.Id);
                if (moduleOneDataToUpdate != null)
                {
                    moduleOneDataToUpdate.FieldOne = moduleOne.FieldOne;
                    moduleOneDataToUpdate.FieldTwo = moduleOne.FieldTwo;
                }
            }
            _context.SaveChanges();
        }
        public void SaveMissionDetails(MissionDetailsHeader Mission)
        {
            if (Mission == null)
                throw new ArgumentNullException();

            if (Mission.Id == 0)
            {
                _context.MRO_MissionHeader.Add(Mission);
            }
            else
            {
                var moduleOneDataToUpdate = _context.MRO_MissionHeader.SingleOrDefault(x => x.Id == Mission.Id);
                if (moduleOneDataToUpdate != null)
                {
                    moduleOneDataToUpdate.Id = Mission.Id;
                    moduleOneDataToUpdate.SubmittedById = Mission.SubmittedById ;
                    moduleOneDataToUpdate.DivisionId = Mission.DivisionId;
                  

                }
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// Delete Module one Details
        /// </summary>
        /// <param name="id"></param>
        public void DeleteModuleOneDetails(int id)
        {
            var moduleOneDataToDelete = _context.Tbl_ModuleOne.SingleOrDefault(x => x.Id == id);
            if (moduleOneDataToDelete != null)
            {
                _context.Tbl_ModuleOne.Remove(moduleOneDataToDelete);
                _context.SaveChanges();
            }
        }
        public void DeleteMMissionDetails(int id)
        {
            var moduleOneDataToDelete = _context.MRO_MissionHeader.SingleOrDefault(x => x.Id == id);
            if (moduleOneDataToDelete != null)
            {
                _context.MRO_MissionHeader.Remove(moduleOneDataToDelete);
                _context.SaveChanges();
            }
        }

        public int getyearasint()
        {
            int result = 0;
           var db = _context.Database; //  .db(); //change the name to whatever your LINQ names are            
            //int nextId = db.SqlQueryRaw<int>("SELECT NEXT VALUE FOR [SeqNewRequest]").FirstOrDefault();


            //var p = new Microsoft.Data.SqlClient.SqlParameter("@result", System.Data.SqlDbType.Int);
            //p.Direction = System.Data.ParameterDirection.Output;
            //_context.Database.ExecuteSqlRaw("set @result = next value for SeqNewRequest", p);
            //var nextId = (int)p.Value;

            string sqlSelect = "select max(right(requestnumber,4))from mstctsrequestheader ";
            var reult = _context.MstctsRequestHeader.FromSqlRaw(sqlSelect).ToList();

            return result;
        }
    //    public JsonResult GetLast5Requests()
    //    {


    //        var RequestDetail= _context.MstctsRequestHeader
    //.OrderByDescending(x => x.RequestId).Select(n => new {n.RequestId,n.RequestNumber,n.ReceivedOnDate,n.TypeOfReqId,n.RequestFrom,n.RequestTo,n.SubProgrammeId,n.StaffResponsible})
    //.Take(5)
    //.ToList();

    //        return new JsonResult(new { value = RequestDetail });
    //    }


    }

}
