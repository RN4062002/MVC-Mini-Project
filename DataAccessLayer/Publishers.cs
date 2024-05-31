using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer
{
    public class Publishers
    {
        public int PublisherID
        {
            get; set;
        }
        public string PublisherName
        {
            get; set;
        }
        public string PublisherCountry
        {
            get; set;
        }
        private Database db;
      

        #region Constructors

        public Publishers()
        {
            db = DatabaseFactory.CreateDatabase();
        }


        public Publishers(int PublisherID)
        {
            db = DatabaseFactory.CreateDatabase();
            this.PublisherID = PublisherID;
        }
        #endregion
        public bool Save()
        {
            if (this.PublisherID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.PublisherID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.PublisherID = 0;
                    return false;
                }
            }
        }

        private bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("PublisherInsert");
                db.AddOutParameter(com, "PublisherID", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.PublisherName))
                {
                    db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
                }
                else
                {
                    db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
                }

                if (!String.IsNullOrEmpty(this.PublisherCountry))
                {
                    db.AddInParameter(com, "PublisherCountry", DbType.String, this.PublisherCountry);
                }
                else
                {
                    db.AddInParameter(com, "PublisherCountry", DbType.String, DBNull.Value);
                }

                db.ExecuteNonQuery(com);
                this.PublisherID = Convert.ToInt32(this.db.GetParameterValue(com, "PublisherID"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return this.PublisherID > 0; // Return whether ID was returned
        }

        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersUpdate");
                this.db.AddInParameter(com, "PublisherID", DbType.Int32, this.PublisherID);
                if (!String.IsNullOrEmpty(this.PublisherName))
                {
                    this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.PublisherCountry))
                {
                    this.db.AddInParameter(com, "PublisherCountry", DbType.String, this.PublisherCountry);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherCountry", DbType.String, DBNull.Value);

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

        public bool Load(int PublisherID)
        {
            try
            {
                
                if (PublisherID != 0)
                {
                    DbCommand com = db.GetStoredProcCommand("PublishersGetDetails");
                    db.AddInParameter(com, "PublisherID", DbType.Int32, PublisherID);
                    DataSet ds = db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.PublisherID = Convert.ToInt32(dt.Rows[0]["PublisherID"]);
                        this.PublisherName = Convert.ToString(dt.Rows[0]["PublisherName"]);
                        this.PublisherCountry = Convert.ToString(dt.Rows[0]["PublisherCountry"]);                    
                    }
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
        }

        public bool Delete(int PublisherID)
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersDelete");
                this.db.AddInParameter(com, "PublisherID", DbType.Int32, PublisherID);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<Publishers> GetList()
        {
            DataSet ds = null;
            try
            {
                DbCommand com = db.GetStoredProcCommand("PublisherGetList");
                ds = db.ExecuteDataSet(com);
                List<Publishers> list = new List<Publishers>();
                foreach (DataRow Row in ds.Tables[0].Rows)
                {
                    list.Add(new Publishers
                    {
                        PublisherID = Convert.ToInt32(Row["PublisherID"]),
                        PublisherName = Convert.ToString(Row["PublisherName"]),
                        PublisherCountry = Convert.ToString(Row["PublisherCountry"])
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public List<string> PublisherDropDownList()
        {
            List<string> publishers = new List<string>();
            try
            {
                DbCommand com = db.GetStoredProcCommand("PublisherDropDown");
                DataSet ds = db.ExecuteDataSet(com);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        publishers.Add(row["PublisherName"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return publishers;
        }
    }
}
