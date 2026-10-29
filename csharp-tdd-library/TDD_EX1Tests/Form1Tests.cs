using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_EX1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/**
 *israel yakuboc -  
 *topaz trabelsi - 
 *max metrikin - 
 ***/
namespace TDD_EX1.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        public TestContext TestContext { get; set; }

        private Form1 form;

        [TestMethod]
        public void AddBookManually_ValidInput_AddsBookToList()
        {
            form = new Form1();
            form.comboBox1.Items.Add("Available");
            form.comboBox1.Items.Add("Borrowed");
            int isbn = 1000001;
            int year = 2020;
            string title = "Test Title";
            string author = "Test Author";
            string category = "Novel";
            string status = "Borrowed";
            int expectedBookCount = 1;
            form.AddBookManually(isbn, year, title, author, category, status);
            Assert.AreEqual(expectedBookCount, form.books.Count);
            var addedBook = form.books.First();

            TestContext.WriteLine($"Title: {addedBook.Title}");
            TestContext.WriteLine($"Title: {addedBook.Author}");
            TestContext.WriteLine($"Category: {addedBook.Category}");
            TestContext.WriteLine($"Status: {addedBook.Status}");
            TestContext.WriteLine($"ISBN: {addedBook.ISBN}");
            TestContext.WriteLine($"Year: {addedBook.Year}");

            Assert.AreEqual("Test Title", addedBook.Title);
            Assert.AreEqual("Test Author", addedBook.Author);
            Assert.AreEqual("Novel", addedBook.Category);
            Assert.AreEqual("Borrowed", addedBook.Status);
            Assert.AreEqual("1000001", addedBook.ISBN.ToString());
            Assert.AreEqual("2020", addedBook.Year.ToString());
        }
        [TestMethod]
        public void AddBookManually_ValidInput_AddsBookToList2()
        {
            form = new Form1();
            form.comboBox1.Items.Add("Available");
            form.comboBox1.Items.Add("Borrowed");

            Random rnd = new Random();
            int expectedBookCount = 1;

            for (int i = 0; i < 100; i++)
            {
                int isbn = rnd.Next(1000000, 10000000);
                form.AddBookManually(isbn, 2020, "Test Title", "Test Author", "Novel", "Borrowed");
                Assert.AreEqual(expectedBookCount + i, form.books.Count);
                var addedBook = form.books[i];
                Assert.AreEqual("Test Title", addedBook.Title);
                Assert.AreEqual("Test Author", addedBook.Author);
                Assert.AreEqual("Novel", addedBook.Category);
                Assert.AreEqual("Borrowed", addedBook.Status);
                Assert.AreEqual(isbn.ToString(), addedBook.ISBN.ToString());
                Assert.AreEqual("2020", addedBook.Year.ToString());
            }


        }

        [TestMethod]
        public void AddBookManually_ValidInput_AddsBookToList3()
        {
            form = new Form1();
            form.comboBox1.Items.Add("Available");
            form.comboBox1.Items.Add("Borrowed");
            int expectedBookCount = 1;
            form.AddBookManually(1000001, 2020, "Test Title", "Test Author", "Novel", "Borrowed");
            Assert.AreEqual(expectedBookCount, form.books.Count);
            var addedBook = form.books.First();
            TestContext.WriteLine($"Title: {addedBook.Title}");
            TestContext.WriteLine($"Title: {addedBook.Author}");
            TestContext.WriteLine($"Category: {addedBook.Category}");
            TestContext.WriteLine($"Status: {addedBook.Status}");
            TestContext.WriteLine($"ISBN: {addedBook.ISBN}");
            TestContext.WriteLine($"Year: {addedBook.Year}");

            Assert.AreEqual("Test Title", addedBook.Title);
            Assert.AreEqual("Test Author", addedBook.Author);
            Assert.AreEqual("Novel", addedBook.Category);
            Assert.AreEqual("Borrowed", addedBook.Status);
            Assert.AreEqual("1000001", addedBook.ISBN.ToString());
            Assert.AreEqual("2020", addedBook.Year.ToString());
        }

        [TestMethod()]
        public void BubbleSortBooksByYearDescendingTest()
        {
            form = new Form1();
            form.books = new List<Book>
            {
                new Book { Year = 2020 },
                new Book { Year = 2019 },
                new Book { Year = 2018 },
                new Book { Year = 2017 }
            };
            var sortedBooks = form.BubbleSortBooksByYearDescending(form.books);
            Assert.AreEqual(2020, sortedBooks[0].Year);
            Assert.AreEqual(2019, sortedBooks[1].Year);
            Assert.AreEqual(2018, sortedBooks[2].Year);
            Assert.AreEqual(2017, sortedBooks[3].Year);
        }

        [TestMethod()]
        public void MergeSortBooksByYearDescendingTest()
        {
            form = new Form1();
            form.books = new List<Book>
            {
                new Book { Year = 2020 },
                new Book { Year = 2019 },
                new Book { Year = 2018 },
                new Book { Year = 2017 }
            };
            var sortedBooks = form.MergeSortBooksByYearDescending(form.books);
            Assert.AreEqual(2020, sortedBooks[0].Year);
            Assert.AreEqual(2019, sortedBooks[1].Year);
            Assert.AreEqual(2018, sortedBooks[2].Year);
            Assert.AreEqual(2017, sortedBooks[3].Year);
        }

        [TestMethod()]
        public void GenerateRandomISBNTest()
        {
            form = new Form1();
            int isbn = form.GenerateRandomISBN();
            Assert.IsTrue(isbn >= 1000000 && isbn <= 10000000);
        }

        [TestMethod()]
        public void DisplayBooksTest()
        {
            form = new Form1();
            form.books = new List<Book>
            {
                new Book { Year = 2020 },
                new Book { Year = 2019 },
                new Book { Year = 2018 },
                new Book { Year = 2017 }
            };
            form.DisplayBooks();
            Assert.AreEqual(4, form.booksDataGridView.Rows.Count);
        }
    }
}
