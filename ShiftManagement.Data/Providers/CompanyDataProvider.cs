using ShiftManagement.Data.Models;

namespace ShiftManagement.Data.Providers
{
    public interface ICompanyDataProvider
    {
        List<Company> GetCompanyList();
        Company GetCompanyById(Guid id);
        bool CheckIfCompanyIdAvailable(string CompanyId);
        bool CreateNewCompany(Company newCompany, out string msg);
        bool DeleteCompany(Guid CompanyId, Guid userid, out string msg);

    }
    public class CompanyDataProvider : ICompanyDataProvider
    {
        private readonly ShiftDbContext _context;
        public CompanyDataProvider(ShiftDbContext context)
        {
            _context = context;

        }

        public List<Company> GetCompanyList()
        {
            return _context.Companies.Where(x => x.IsDeleted == false && x.IsMasterCompany == false).OrderBy(x => x.CompanyName).ToList();
        }
        public Company GetCompanyById(Guid id)
        {
            return _context.Companies.FirstOrDefault(x => x.IsDeleted == false && x.IsMasterCompany == false && x.Id == id);
        }
        public bool CheckIfCompanyIdAvailable(string CompanyId)
        {
            return !_context.Companies.Any(x=> x.CompanyNameID == CompanyId);
        }
        public bool CreateNewCompany(Company newCompany, out string msg)
        {
            bool IsSuccess = false;
            msg = "";
            if (newCompany != null)
            {               
                    // Check if account name already exists
                    var exacc = _context.Companies.Any(x => x.IsDeleted == false && (x.CompanyName.ToLower() == newCompany.CompanyName.ToLower() || x.CompanyNameID.ToLower() == newCompany.CompanyNameID.ToLower()));
                    if (exacc == false)
                    {                                                
                        _context.Companies.Add(newCompany);
                        _context.SaveChanges();
                        msg = $"Company created successfully.";
                        IsSuccess = true;
                    }
                    else
                    {
                        msg = "Company name or ID already exists.";
                    }               
            }
            else
            {
                msg = "Invalid company details.";
            }

            return IsSuccess;
        }

        public bool DeleteCompany(Guid CompanyId,Guid userid, out string msg)
        {
            bool IsSuccess = false;
            msg = "";
            var exacc = _context.Companies.FirstOrDefault(x => x.IsDeleted == false && x.Id == CompanyId);
            if (exacc != null)
            {
                exacc.IsDeleted = true;
                exacc.DeletionDate = DateTime.UtcNow;
                exacc.DeleteUserID = userid;
                _context.Companies.Update(exacc);
                _context.SaveChanges();
                msg = $"Company deleted successfully.";
                IsSuccess = true;
            }
            else
            {
                msg = "Company does not exists.";
            }


            return IsSuccess;
        }
    }
}
