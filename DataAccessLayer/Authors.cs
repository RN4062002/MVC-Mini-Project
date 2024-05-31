using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace DataAccessLayer
{
   public class Authors
    {

        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhone { get; set; }


        private Database db;
       // Authors authors = new Authors();

        #region Constructors

        public Authors()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }


        public Authors(int AuthorID)
        {
            db = DatabaseFactory.CreateDatabase();
            this.AuthorID = AuthorID;
        }
        #endregion
        public bool Save()
        {
            if (this.AuthorID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.AuthorID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.AuthorID = 0;
                    return false;
                }
            }
        }

        private bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("AuthorInsert");
                db.AddOutParameter(com, "AuthorID", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.AuthorName))
                {
                    db.AddInParameter(com, "AuthorName", DbType.String, this.AuthorName);
                }
                else
                {
                    db.AddInParameter(com, "AuthorName", DbType.String, DBNull.Value);
                }

                if (!String.IsNullOrEmpty(this.AuthorPhone))
                {
                    db.AddInParameter(com, "AuthorPhone", DbType.String, this.AuthorPhone);
                }
                else
                {
                    db.AddInParameter(com, "AuthorPhone", DbType.String, DBNull.Value);
                }

                db.ExecuteNonQuery(com);
                this.AuthorID = Convert.ToInt32(this.db.GetParameterValue(com, "AuthorID"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return this.AuthorID > 0; // Return whether ID was returned
        }

        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("AuthorUpdate");
                this.db.AddInParameter(com, "AuthorID", DbType.Int32, this.AuthorID);
                if (!String.IsNullOrEmpty(this.AuthorName))
                {
                    this.db.AddInParameter(com, "AuthorName", DbType.String, this.AuthorName);
                }
                else
                {
                    this.db.AddInParameter(com, "AuthorName", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.AuthorPhone))
                {
                    this.db.AddInParameter(com, "AuthorPhone", DbType.String, this.AuthorPhone);
                }
                else
                {
                    this.db.AddInParameter(com, "AuthorPhone", DbType.String, DBNull.Value);

                }
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public bool Load(int AuthorID)
        {
            try
            {
               
                if (AuthorID != 0)
                {
                    DbCommand com = db.GetStoredProcCommand("AuthorGetDetails");
                    db.AddInParameter(com, "AuthorID", DbType.Int32, AuthorID);
                    DataSet ds = db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.AuthorID = Convert.ToInt32(dt.Rows[0]["AuthorID"]);
                        this.AuthorName = Convert.ToString(dt.Rows[0]["AuthorName"]);
                        this.AuthorPhone = Convert.ToString(dt.Rows[0]["AuthorPhone"]);
                        return true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
        }

        public bool Delete(int AuthorID)
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("AuthorDelete");
                this.db.AddInParameter(com, "AuthorID", DbType.Int32, AuthorID);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<Authors> GetList()
        {
            DataSet ds = null;
            try
            {
                DbCommand com = db.GetStoredProcCommand("AuthorGetList");
                ds = db.ExecuteDataSet(com);
                List<Authors> list = new List<Authors>();
                foreach (DataRow Row in ds.Tables[0].Rows)
                {
                    list.Add(new Authors
                    {
                        AuthorID = Convert.ToInt32(Row["AuthorID"]),
                        AuthorName = Convert.ToString(Row["AuthorName"]),
                        AuthorPhone = Convert.ToString(Row["AuthorPhone"])
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
