using AntDemoWeb.Common;
using AntDemoWeb.Models;
using AntDemoWeb.Service;
using AntDemoWeb.ViewModels;
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

        public BookController(BookService bookService)
        {
            this.bookService = bookService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddBook(BookAddModel addModel)
        {
            try
            {
                ValidateAddBookModel(addModel);

                Book model = NewBook();

                model.BookName = addModel.BookName;
                model.BookPath = bookService.GetUploadFilePath();
                model.ConvertStatus = Enum.ConvertStatusEnum.UnStart;
                model.DeleteFlag = Enum.DeleteFlagEnum.UnDeleted;
                model.UploadTime = DateTime.Now;

                var bookId = bookService.AddBook(model);
                ViewBag.BookId = bookId;
                return Json(new { code = 1 });
            }
            catch (ValidateException ex)
            {
                return Json(new { code = ex.Code, msg = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = ex.Message });
            }

        }

        private void ValidateAddBookModel(BookAddModel addModel)
        {
            if (string.IsNullOrEmpty(addModel.BookName))
                throw new ValidateException(401, "请输入书籍名称");
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