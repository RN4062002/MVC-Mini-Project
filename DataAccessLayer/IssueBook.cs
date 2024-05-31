using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccessLayer
{

    public class IssueBook
    {
        #region Properties
        public int IssueBookID { get; set; }
        public int StudentID { get; set; }
        public string IssueBookDate { get; set; }
        public string BookQuentityList { get; set; }
        public int BookID { get; set; }
        public int Quentity { get; set; }
        public string PublisherName { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public List<IssueBook> issueBookList { get; set; }
        #endregion

        private Database db;

        #region Constructors

        public IssueBook()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }

        public IssueBook(int IssueBookID)
        {
            db = DatabaseFactory.CreateDatabase();
            this.IssueBookID = IssueBookID;
        }
        #endregion

        public bool Save()
        {
            if (this.IssueBookID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.IssueBookID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.IssueBookID = 0;
                    return false;
                }
            }
        }

        private bool Update()
        {
            throw new NotImplementedException();
        }

        private bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("issueBookInsert");

                if (!String.IsNullOrEmpty(this.IssueBookDate))
                {
                    db.AddInParameter(com, "issueBookDate", DbType.String, this.IssueBookDate);
                }
                else
                {
                    db.AddInParameter(com, "issueBookDate", DbType.String, DBNull.Value);
                }
                if ((BookQuentityList != null))
                {

                    db.AddInParameter(com, "BookQuentityList", DbType.Xml, BookQuentityList);
                }
                else
                {
                    db.AddInParameter(com, "BookQuentityList", DbType.Xml, DBNull.Value);
                }
                if ((this.StudentID > 0))
                {
                    db.AddInParameter(com, "studentID", DbType.Int32, this.StudentID);
                }
                else
                {
                    db.AddInParameter(com, "studentID", DbType.Int32, DBNull.Value);
                }

                db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public List<IssueBook> GetIssuBookList()
        {
            DataSet ds = null;
            try
            {
                DbCommand com = db.GetStoredProcCommand("GetIssueBooksDetails");
                db.AddInParameter(com, "StudentID", DbType.String, this.StudentID);
                ds = db.ExecuteDataSet(com);

                List<IssueBook> list = new List<IssueBook>();
                #region
                foreach (DataRow Row in ds.Tables[0].Rows)
                {

                    list.Add(new IssueBook
                    {

                        BookID = Convert.ToInt32(Row["BookID"]),
                        BookName = Convert.ToString(Row["BookName"]),
                        PublisherName = Convert.ToString(Row["PublisherName"]),
                        AuthorName = Convert.ToString(Row["AuthorName"]),
                        Quentity = Convert.ToInt32(Row["Quentity"])
                    });
                }

                return list;
            }
            #endregion


            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

