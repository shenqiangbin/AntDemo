using AntDemoWeb.Common;
using AntDemoWeb.Enum;
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

        #region 新增图书

        [HttpPost]
        public ActionResult AddBook(BookAddModel addModel)
        {
            try
            {
                ValidateAddBookModel(addModel);

                Book model = NewBook();

                model.BookName = addModel.BookName;

                string pdfDir = bookService.GetUploadDir();
                //Directory.CreateDirectory(pdfDir);
                model.BookPath = pdfDir + addModel.File.FileName;

                string saveDir = bookService.GetSWFDir();
                //Directory.CreateDirectory(saveDir);
                model.SwfPath = saveDir + $"{Guid.NewGuid().ToString().Replace("-", "")}.swf";

                model.ConvertStatus = Enum.ConvertStatusEnum.UnStart;
                model.DeleteFlag = Enum.DeleteFlagEnum.UnDeleted;
                model.UploadTime = DateTime.Now;

                var bookId = bookService.AddBook(model);
                ViewBag.BookId = bookId;

                SaveFileToDisk(addModel.File, model.BookPath);
                Task.Run(() =>
                {
                    try
                    {
                        bookService.UpdateBookConvertStatus(bookId, ConvertStatusEnum.Converting);
                        ConvertPdfToSWF(model.BookPath, model.SwfPath);
                        bookService.UpdateBookConvertStatus(bookId, ConvertStatusEnum.Finished);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"swf:{model.SwfPath} {ex.Message}");
                        bookService.UpdateBookConvertStatus(bookId, ConvertStatusEnum.Failed);
                    }
                });

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

        //验证
        private void ValidateAddBookModel(BookAddModel addModel)
        {
            if (string.IsNullOrEmpty(addModel.BookName))
                throw new ValidateException(401, "请输入书籍名称");
        }

        //保存文件
        private void SaveFileToDisk(HttpPostedFileBase file, string bookPath)
        {

            //保存文件（注：这里使用Task.Run的异步形式并不管用，还是必须在文件保存成功之后，前台界面才会有响应。）
            var fileSavePath = Server.MapPath(bookPath);
            file.SaveAs(fileSavePath);

            //StreamReader sr = new StreamReader(addModel.File.InputStream, System.Text.Encoding.Default);
            //while (!sr.EndOfStream) {
            //   var s = sr.ReadLine();
            //}            
        }

        //转换文件
        private void ConvertPdfToSWF(string pdfPath, string swfPath)
        {
            string cmdStr = "C:/Program Files/SWFTools/pdf2swf.exe";
            if (!System.IO.File.Exists(cmdStr))
                cmdStr = "C:/Program Files (x86)/SWFTools/pdf2swf.exe";
            if (!System.IO.File.Exists(cmdStr))
                throw new Exception("请安装pdf2swf");

            string args = BuildAgrs(Server.MapPath(pdfPath), Server.MapPath(swfPath));
            string result = CmdHelper.ExecutCmd(cmdStr, args);
            Logger.Log(result);
        }

        //转换文件-构建转换参数
        private string BuildAgrs(string filePath, string savePath)
        {
            string path = Server.MapPath("/pdfView/xpdf/chinese-simplified");
            //args = " -t d:/1.pdf -o d:/1.swf -T 9 -f";// -T 9 表示版本9 -f 实现搜索时，高亮显示    
            return $" -t {filePath} -o {savePath} -T 9 -f  -s languagedir={path} -s storeallcharacters";
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

        #endregion  

        public ActionResult Test()
        {
            PdfConvertor.AddTask(Guid.NewGuid().ToString());
            return Content(DateTime.Now.ToString());
        }
    }
}