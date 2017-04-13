using AntDemoWeb.Common;
using AntDemoWeb.Models;
using AntDemoWeb.Service;
using AntDemoWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult AddBook(BookAddModel addModel)
        {
            try
            {
                ValidateAddBookModel(addModel);

                Book model = NewBook();

                model.BookName = addModel.BookName;
                model.BookPath = bookService.GetUploadFilePath(addModel.File.FileName);
                model.ConvertStatus = Enum.ConvertStatusEnum.UnStart;
                model.DeleteFlag = Enum.DeleteFlagEnum.UnDeleted;
                model.UploadTime = DateTime.Now;

                var bookId = bookService.AddBook(model);
                ViewBag.BookId = bookId;

                SaveFileToDisk(addModel, model);

                //return Json(new { code = 1 });
                ViewBag.Msg = "上传成功";
                return View();
            }
            catch (ValidateException ex)
            {
                ViewBag.Msg = ex.Message;
                //return Json(new { code = ex.Code, msg = ex.Message });
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                //return Json(new { code = 0, msg = ex.Message });
                return View();
            }

        }

        private void SaveFileToDisk(BookAddModel addModel, Book model)
        {

            //保存文件（注：这里使用Task.Run的异步形式并不管用，还是必须在文件保存成功之后，前台界面才会有响应。）
            var fileSavePath = Server.MapPath(model.BookPath);
            addModel.File.SaveAs(fileSavePath);

            //StreamReader sr = new StreamReader(addModel.File.InputStream, System.Text.Encoding.Default);
            //while (!sr.EndOfStream) {
            //   var s = sr.ReadLine();
            //}
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

        public ActionResult Test()
        {
            PdfConvertor.AddTask(Guid.NewGuid().ToString());
            return Content(DateTime.Now.ToString());
        }
    }
}