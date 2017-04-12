using AntDemoWeb.Models;
using AntDemoWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.Service
{
    public class BookService
    {
        private BookRepository bookRepository;

        public BookService(BookRepository bookingRepository)
        {
            bookRepository = bookingRepository;
        }

        public int AddBook(Book book)
        {
            var bookId = bookRepository.Add(book);
            return bookId;
        }

        public string GetUploadFilePath()
        {
            return "/PDF";
        }
    }
}