using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public class BookQuentityList
    {
        public int BookID { get; set; }
        public int Quentity { get; set; }
    }
    public class StudentsModel
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Totalcount { get; set; }
        public int PageNumber { get; set; }
        public List<Students> StudentsList { get; set; }
        public List<Books> bookList {get; set;}
        public string IssueBookDate { get; set; }
        public List<BookQuentityList> BookQuentityList { get; set; }
        public int BookID { get; set; }
        public int Quentity { get; set; }
        public string PublisherName { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public List<IssueBook> issueBookList { get; set; }
    }
}
