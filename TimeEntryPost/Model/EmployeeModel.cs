using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeEntryPost.Model
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
    }
}
