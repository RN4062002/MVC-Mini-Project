using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace DataAccessLayer
{

    public class Books
    {
        #region Properties
        public int counter;
        public int BookID
        {
            get; set;
        }
        public string BookName
        {
            get; set;
        }
        public int? BookPrice
        {
            get; set;
        }
        public int? BookStock
        {
            get; set;
        }
        public string PublisherName
        {
            get; set;
        }
        public string AuthorName
        {
            get; set;
        }
        public int PublisherID
        {
            get; set;
        }
        public int AuthorID
        {
            get; set;
        }
        public int PageSize
        {
            get; set;
        }
        public int PageNumber
        {
            get; set;
        }
        public int TotalRecordes;
        public string MultiPublisherIDList
        {
            get; set;
        }
        public string MultiAuthorIDList
        {
            get; set;
        }
        //public string SearchByAuthor { get; set; }
        #endregion

        private Database db;

        #region Constructors

        public Books()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }


        public Books(int BookID)
        {
            db = DatabaseFactory.CreateDatabase();
            this.BookID = BookID;
        }
        #endregion

        public bool Save()
        {
            if (this.BookID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.BookID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.BookID = 0;
                    return false;
                }
            }
        }

        private bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("BookInsert");
                db.AddOutParameter(com, "BookID", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.BookName))
                {
                    db.AddInParameter(com, "BookName", DbType.String, this.BookName);
                }
                else
                {
                    db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
                }
                if ((this.BookPrice > 0))
                {
                    db.AddInParameter(com, "BookPrice", DbType.Int32, this.BookPrice);
                }
                else
                {
                    db.AddInParameter(com, "BookPrice", DbType.Int32, DBNull.Value);
                }
                if ((this.BookStock > 0))
                {
                    db.AddInParameter(com, "BookStock", DbType.String, this.BookStock);
                }
                else
                {
                    db.AddInParameter(com, "BookStock", DbType.String, DBNull.Value);
                }
                if ((this.PublisherID > 0))
                {
                    db.AddInParameter(com, "PublisherID", DbType.String, this.PublisherID);
                }
                else
                {
                    db.AddInParameter(com, "PublisherID", DbType.String, DBNull.Value);
                }
                if (this.AuthorID > 0)
                {
                    db.AddInParameter(com, "AuthorID", DbType.String, this.AuthorID);
                }
                else
                {
                    db.AddInParameter(com, "AuthorID", DbType.String, DBNull.Value);
                }

                db.ExecuteNonQuery(com);
                this.BookID = Convert.ToInt32(this.db.GetParameterValue(com, "BookID"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return this.BookID > 0; // Return whether ID was returned
        }

        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("BooksUpdate");
                this.db.AddInParameter(com, "BookID", DbType.Int32, this.BookID);
                if (!String.IsNullOrEmpty(this.BookName))
                {
                    this.db.AddInParameter(com, "BookName", DbType.String, this.BookName);
                }
                else
                {
                    this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
                }

                if (this.BookPrice > 0)
                {
                    this.db.AddInParameter(com, "BookPrice", DbType.Int32, this.BookPrice);
                }
                else
                {
                    this.db.AddInParameter(com, "BookPrice", DbType.Int32, DBNull.Value);
                }
                if (this.BookStock > 0)
                {
                    this.db.AddInParameter(com, "BookStock", DbType.Int32, this.BookStock);
                }
                else
                {
                    this.db.AddInParameter(com, "BookStock", DbType.Int32, DBNull.Value);
                }
                if (this.PublisherID > 0)
                {
                    this.db.AddInParameter(com, "PublisherID", DbType.String, this.PublisherID);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherID", DbType.String, DBNull.Value);
                }
                if (this.AuthorID > 0)
                {
                    this.db.AddInParameter(com, "AuthorID", DbType.String, this.AuthorID);
                }
                else
                {
                    this.db.AddInParameter(com, "AuthorID", DbType.String, DBNull.Value);
                }
               
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Load()
        {
            try
            {
                Books book = new Books();
                if (BookID != 0)
                {
                    DbCommand com = db.GetStoredProcCommand("BooksGetDetails");
                    db.AddInParameter(com, "BookID", DbType.Int64, this.BookID);
                    DataSet ds = db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        BookID = Convert.ToInt32(dt.Rows[0]["BookID"]);
                        BookName = Convert.ToString(dt.Rows[0]["BookName"]);
                        BookPrice = Convert.ToInt32(dt.Rows[0]["BookPrice"]);
                        BookStock = Convert.ToInt32(dt.Rows[0]["BookStock"]);
                        PublisherID = Convert.ToInt32(dt.Rows[0]["PublisherID"]);
                        PublisherName = Convert.ToString(dt.Rows[0]["PublisherName"]);
                        AuthorName = Convert.ToString(dt.Rows[0]["AuthorName"]);
                        AuthorID = Convert.ToInt32(dt.Rows[0]["AuthorID"]);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("BooksDelete");
                this.db.AddInParameter(com, "BookID", DbType.Int32, BookID);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<Books> GetList()
        {
            DataSet ds = null;
            try
            {
                DbCommand com = db.GetStoredProcCommand("BooksGetList");
                db.AddInParameter(com, "search", DbType.String, this.BookName);
                //if (PublisherID > 0)
                //{
                //    db.AddInParameter(com, "PublisherSearch", DbType.Int32, this.PublisherID);
                //}
                //else
                //{
                //    db.AddInParameter(com, "PublisherSearch", DbType.Int32, DBNull.Value);
                //}
                //if (this.AuthorID > 0)
                //{
                //    db.AddInParameter(com, "AuthorSearch", DbType.Int32, this.AuthorID);
                //}
                //else
                //{
                //    db.AddInParameter(com, "AuthorSearch", DbType.Int32, DBNull.Value);
                //}
                if (this.MultiPublisherIDList != null)
                {
                    db.AddInParameter(com, "PublisherSearch", DbType.String, this.MultiPublisherIDList);
                }
                else
                {
                    db.AddInParameter(com, "PublisherSearch", DbType.String, DBNull.Value);
                }
                if (this.MultiAuthorIDList != null)
                {
                    db.AddInParameter(com, "AuthorSearch", DbType.String, this.MultiAuthorIDList);
                }
                else
                {
                    db.AddInParameter(com, "AuthorSearch", DbType.String, DBNull.Value);
                }
                //if (SearchByAuthor != null)
                //{
                //    db.AddInParameter(com, "AuthorSearch", DbType.String, this.SearchByAuthor);
                //}
                //else
                //{
                //    db.AddInParameter(com, "AuthorSearch", DbType.String, DBNull.Value);
                //}
                if (this.PageSize > 0)
                {
                    db.AddInParameter(com, "PageSize", DbType.Int32, this.PageSize);
                }
                else
                {
                    db.AddInParameter(com, "PageSize", DbType.Int32, this.PageSize = 10);
                }
                if (this.PageNumber > 0)
                {
                    db.AddInParameter(com, "PageNumber", DbType.Int32, this.PageNumber);
                }
                else
                {
                    db.AddInParameter(com, "PageNumber", DbType.Int32, 1);
                }
                db.AddOutParameter(com, "TotalCount", DbType.Int32, 1024);
                db.AddOutParameter(com, "TotalRows", DbType.Int32, 1024);

                ds = db.ExecuteDataSet(com);
                
                counter = (int)db.GetParameterValue(com, "TotalCount");
                TotalRecordes = (int)db.GetParameterValue(com, "TotalRows");
                List<Books> list = new List<Books>();
                #region
                foreach (DataRow Row in ds.Tables[0].Rows)
                {

                    list.Add(new Books
                    {

                        BookID = Convert.ToInt32(Row["BookID"]),
                        BookName = Convert.ToString(Row["BookName"]),
                        BookPrice = Convert.ToInt32(Row["BookPrice"]),
                        BookStock = Convert.ToInt32(Row["BookStock"]),
                        PublisherName = Convert.ToString(Row["PublisherName"]),
                        AuthorName = Convert.ToString(Row["AuthorName"])
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
