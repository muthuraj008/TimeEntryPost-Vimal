using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeEntryPost.Model;

namespace TimeEntryPost.IRepository
{
    public interface IEmployeeDetailsRepository
    {
        public string AddToDatabase(List<EmployeeModel> model);
        public List<EmployeeModel> GetAllData();
    }
}
