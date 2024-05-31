using Model;
using System.Web.Mvc;
using DataAccessLayer;
namespace BookApp2.Controllers
{
    public class IndexController : Controller
    {
        Books book;
        Publishers publishers = new Publishers();
        Authors authors = new Authors();

        public ActionResult Index(BooksModel bookmodel)
        {
            book = new Books();
            if (Session["BookModel"] != null)  
            {
                bookmodel = (BooksModel)Session["BookModel"];
     
                if (bookmodel.MultiPublisherIDList != null)
                {
                    book.MultiPublisherIDList = string.Join(",", bookmodel.MultiPublisherIDList); ;
                }
                else
                {
                    bookmodel.MultiPublisherIDList = null;
                }
                if (bookmodel.MultiAuthorIDList != null)
                {
                    book.MultiAuthorIDList = string.Join(",", bookmodel.MultiAuthorIDList); ;
                }
                else
                {
                    bookmodel.MultiAuthorIDList = null;
                }
                book.PageNumber = bookmodel.PageNumber;
                book.PageSize = bookmodel.PageSize;
            }
           
            bookmodel.PublishersList = publishers.GetList();
            bookmodel.AuthorsList = authors.GetList();
            bookmodel.bookList = book.GetList();
           
            return View(bookmodel);
        }

        public ActionResult Create(BooksModel bookmodel)
        {
            book = new Books();
            book.PublisherID = bookmodel.PublisherID;
            book.AuthorID = bookmodel.AuthorID;
            book.BookName = bookmodel.BookName;
            book.BookPrice = bookmodel.BookPrice;
            book.BookStock = bookmodel.BookStock;
            bookmodel.PublishersList = publishers.GetList();
            bookmodel.AuthorsList = authors.GetList();
            bool check = book.Save();
            if (check)
            {
                return Redirect("Index");
            }
            else
            {
                return View(bookmodel);
            }
        }

        public ActionResult Insert()
        {
            BooksModel bookmodel = new BooksModel();
            book = new Books();
            book.PublisherID = bookmodel.PublisherID;
            book.AuthorID = bookmodel.AuthorID;
            book.BookName = bookmodel.BookName;
            book.BookPrice = bookmodel.BookPrice;
            book.BookStock = bookmodel.BookStock;
            bookmodel.PublishersList = publishers.GetList();
            bookmodel.AuthorsList = authors.GetList();
            bool check = book.Save();
            if (check)
            {
                return Json(new { success = true, message = "Error adding book" });
            }
            else
            {
                return PartialView("_InsertEditBook", bookmodel);
            }
        }

        [HttpPost]
        public JsonResult Insert(BooksModel bookmodel)
        {
            book = new Books();
            book.PublisherID = bookmodel.PublisherID;
            book.AuthorID = bookmodel.AuthorID;
            book.BookName = bookmodel.BookName;
            book.BookPrice = bookmodel.BookPrice;
            book.BookStock = bookmodel.BookStock;
            bookmodel.PublishersList = publishers.GetList();
            bookmodel.AuthorsList = authors.GetList();
            bool check = book.Save();
            if (check)
            {
                return Json(new { success = true, message = " adding book" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Error adding book" });
            }
        }

        public ActionResult Edit(BooksModel bookmodel)
        {
            book = new Books();
            book.BookID = bookmodel.BookID; 
            book.Load();
            bookmodel.BookName = book.BookName;
            bookmodel.BookPrice = book.BookPrice;
            bookmodel.BookStock = book.BookStock;
            bookmodel.PublisherID = book.PublisherID;
            bookmodel.AuthorID = book.AuthorID;
            bookmodel.PublishersList = publishers.GetList();
            bookmodel.AuthorsList = authors.GetList();
            return View(bookmodel);
        }
        [HttpPost]
        public ActionResult Edit(BooksModel bookModel, Books book)
        {
            book.BookID = bookModel.BookID;
            book.PageNumber = bookModel.PageNumber;
            bool check = book.Save();
            if (check)
            {
                return Redirect("Index");
            }
            else
            {
                return View();          
            }
        }

        public ActionResult Update(BooksModel bookModel)
        {
            BooksModel bookmodel = new BooksModel();
            book = new Books();
            book.BookID = bookModel.BookID;
            book.Load();
            bookmodel.BookName = book.BookName;
            bookmodel.BookPrice = book.BookPrice;
            bookmodel.BookStock = book.BookStock;
            bookmodel.PublisherID = book.PublisherID;
            bookmodel.AuthorID = book.AuthorID;
            bookmodel.PublishersList = publishers.GetList();
            bookmodel.AuthorsList = authors.GetList();
            return PartialView("_InsertEditBook", bookmodel);
        }
        [HttpPost]
        public JsonResult Update(BooksModel bookModel, Books book)
        {
            book.BookID = bookModel.BookID;
            book.PageNumber = bookModel.PageNumber;
            bool check = book.Save();
            if (check)
            {
                return Json(new { success = true, message = " Editing book" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Error adding book" });
            }
        }

        public ActionResult Delete(BooksModel bookmodel)
        {
            if (bookmodel.BookID > 0)
            {
                book = new Books();
                book.BookID = bookmodel.BookID;
                book.Delete();
                return RedirectToAction("GetBookList");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult GetBookList(BooksModel bookmodel)
        {
            book = new Books();
            var Model = new BooksModel();

            Session["BookModel"] = bookmodel;

            book.BookName = bookmodel.BookName;
            book.PublisherID = bookmodel.PublisherID;
            book.AuthorID = bookmodel.AuthorID;
            book.PageSize = bookmodel.PageSize;
            book.PageNumber = bookmodel.PageNumber;
            if (bookmodel.MultiPublisherIDList != null)
            {
                book.MultiPublisherIDList = string.Join(",", bookmodel.MultiPublisherIDList); 
            }
            else
            {
                bookmodel.MultiPublisherIDList = null;
            }
            if (bookmodel.MultiAuthorIDList != null)
            {
                book.MultiAuthorIDList = string.Join(",", bookmodel.MultiAuthorIDList); ;
            }
            else
            {
                bookmodel.MultiAuthorIDList = null;
            }
            bookmodel.bookList = book.GetList();
            bookmodel.TotalRecordes = book.TotalRecordes;
            bookmodel.PageSize = book.PageSize;
            bookmodel.counter = book.counter;
            
            return Json(bookmodel, JsonRequestBehavior.AllowGet);
        }
    }
}
