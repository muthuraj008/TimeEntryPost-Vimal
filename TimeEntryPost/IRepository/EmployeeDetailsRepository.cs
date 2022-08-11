using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeEntryPost.Model;

namespace TimeEntryPost.IRepository
{
    public class EmployeeDetailsRepository : IEmployeeDetailsRepository
    {
        public EmployeeDetailsRepository()
        {

        }

        public string AddToDatabase(List<EmployeeModel> model)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("ATT_EMPID", typeof(int));
            table.Columns.Add("ATT_DATE", typeof(DateTime));
            table.Columns.Add("ATT_INTIME", typeof(DateTime));
            table.Columns.Add("ATT_OUTTIME", typeof(DateTime));

            for (int i = 0; i < model.Count; i++)
                table.Rows.Add(new object[] {
                            model[i].EmployeeId,
                            model[i].WorkDate,
                            model[i].InTime,
                            model[i].OutTime
                });


            using (SqlConnection con = new SqlConnection(@"Server=NLTI175\NLTI175;User Id=sa;Password=Admin2022;Database=ATTDATA;MultipleActiveResultSets=true"))
            {
                con.Open();
                using (System.Data.SqlClient.SqlTransaction tran = con.BeginTransaction())
                {
                    var options = new SqlBulkCopyOptions();
                    var copy = new SqlBulkCopy(con, options, tran);
                    copy.DestinationTableName = "dbo.ATT_Data";
                    // Add mappings so that the column order doesn't matter
                    copy.ColumnMappings.Add("ATT_EMPID", "ATT_EMPID");
                    copy.ColumnMappings.Add("ATT_DATE", "ATT_DATE");
                    copy.ColumnMappings.Add("ATT_INTIME", "ATT_INTIME");
                    copy.ColumnMappings.Add("ATT_OUTTIME", "ATT_OUTTIME");
                    try
                    {
                        copy.WriteToServer(table);
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                    }
                }
            }
            return "added";
        }

        public List<EmployeeModel> GetAllData()
        {
            using (SqlConnection con = new SqlConnection(@"Server=NLTI175\NLTI175;User Id=sa;Password=Admin2022;Database=ATTDATA;MultipleActiveResultSets=true"))
            {
                con.Open();
                var query = "select * from [ATTDATA].[dbo].[ATT_Data] where CONVERT(VARCHAR(20),ATT_DATE,101) = CONVERT(VARCHAR(20),GETDATE()-1,101)";
                SqlCommand oCmd = new SqlCommand(query, con);
            }
            return null;
        }
    }
}
