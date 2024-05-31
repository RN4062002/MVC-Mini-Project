using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Students
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        private Database db;
        #region Constructors
        public Students()
        {
            db = DatabaseFactory.CreateDatabase();
        }
        public Students(int PublisherID)
        {
            db = DatabaseFactory.CreateDatabase();
            this.StudentID = StudentID;
        }
        #endregion

        public List<Students> GetList()
        {
            DataSet ds = null;
            try
            {
                DbCommand com = db.GetStoredProcCommand("StudentGetList");
                ds = db.ExecuteDataSet(com);
                List<Students> list = new List<Students>();
                foreach (DataRow Row in ds.Tables[0].Rows)
                {
                    list.Add(new Students
                    {
                        StudentID = Convert.ToInt32(Row["StudentID"]),
                        StudentName = Convert.ToString(Row["StudentName"])
                      
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
