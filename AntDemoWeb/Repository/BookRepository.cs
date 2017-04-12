using AntDemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.Repository
{
    public class BookRepository
    {
        //增改查
        public int Add(Book book)
        {
            string cmdText = "insert into book values(?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    null,  //对应的主键不要赋值了
                    book.BookName,
                    book.BookPath,
                    book.ConvertStatus,
                    book.SwfPath,
                    book.UploadTime,
                    book.DeleteFlag
            };
            object result = SQLiteHelper.ExecuteScalar(cmdText, paramList);

            int intResult;
            if (int.TryParse(result.ToString(),out intResult))
                return intResult;
            return 0;
        }
    }
}