using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using DataAccessLayer;
using System.IO;
using System.Xml.Serialization;

namespace BookApp2.Controllers
{
    public class IssueBooksController : Controller
    {
        // GET: IssueBooks
        public ActionResult IssueBooksIndex(StudentsModel studentsmodel)
        {
            try
            {
                IssueBook issuebook = new IssueBook();
                Students students = new Students();
                studentsmodel.StudentsList = students.GetList();
                issuebook.StudentID = studentsmodel.StudentID;
                studentsmodel.issueBookList = issuebook.GetIssuBookList();
                return View(studentsmodel);
            }
            catch (Exception e)
            {
                return Json(new { message = " Page not found error = " + e }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult BookGetList(StudentsModel studentsmodel)
        {
            try
            {
                Books book = new Books();
                book.PageNumber = studentsmodel.PageNumber;
                studentsmodel.bookList = book.GetList();
                studentsmodel.Totalcount = book.counter;

                return Json(studentsmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = " Page not found error = " + e }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult BookList()
        {
            return PartialView("_BookList");
        }

        [HttpPost]
        public JsonResult Insert(StudentsModel studentsmodel)
        {
            try
            {
                IssueBook issueBook = new IssueBook();
                issueBook.StudentID = studentsmodel.StudentID;
                issueBook.IssueBookDate = studentsmodel.IssueBookDate;
                issueBook.BookQuentityList = ConvertBookQuentityListToXml(studentsmodel.BookQuentityList);
                bool check = issueBook.Save();
                if (check)
                {
                    return Json(new { success = true, message = " Issuing book successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Error issuing book" });
                }
            }catch(Exception e)
            {
                 return Json(new { success = false, message = "Error issuing book" });
            }
            

        }

        public ActionResult GetIssueBooksDetails(StudentsModel studentsmodel)
        {
            IssueBook issuebook = new IssueBook();
            try
            {
                issuebook.StudentID = studentsmodel.StudentID;
                studentsmodel.issueBookList = issuebook.GetIssuBookList();
                return Json(studentsmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {                
                return View(studentsmodel);              
            }
        }

        private string ConvertBookQuentityListToXml(List<BookQuentityList> bookQuentityList)
        {
            // Implement conversion of the list to XML
            // This can be done using XML serialization or manually constructing the XML string
            // Here is an example using XML serialization
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BookQuentityList>));
                serializer.Serialize(sw, bookQuentityList);
                return sw.ToString();
            }
        }
    }
}