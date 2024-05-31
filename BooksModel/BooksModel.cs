
using DataAccessLayer;
using System.Collections.Generic;

namespace Model
{
    public class BooksModel
    {

        #region Properties
      
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
        } = 10;
        public int counter
        {
            get; set;
        }
        public int PageNumber
        {
            get; set;
        } = 1;

        public int TotalRecordes
        {
            get; set;
        }


        public List<Books> bookList
        {
            get; set;
        }
        public List<Publishers> PublishersList
        {
            get; set;
        }
        public List<Authors> AuthorsList
        {
            get; set;
        }
        public List<int> MultiPublisherIDList
        {
            get; set;
        }
        public List<int> MultiAuthorIDList
        {
            get; set;
        }

        

        #endregion
    }
}
