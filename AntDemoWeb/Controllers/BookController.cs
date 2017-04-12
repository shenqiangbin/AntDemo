using AntDemoWeb.Models;
using AntDemoWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntDemoWeb.Controllers
{
    public class BookController : Controller
    {
        private BookService bookService;

        public BookController()
        {
            bookService = new BookService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBook()
        {
            Book model = NewBook();
            var bookId = bookService.AddBook(model);
            ViewBag.BookId = bookId;
            return View();
        }

        private Book NewBook()
        {
            Book book = new Book();

            book.BookName = "1.pdf";
            book.BookPath = "/pdf";
            book.ConvertStatus = Enum.ConvertStatusEnum.UnStart;
            book.UploadTime = DateTime.Now;
            book.DeleteFlag = Enum.DeleteFlagEnum.UnDeleted;

            return book;
        }
    }
}