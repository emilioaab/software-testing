using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

/**
 *israel yakuboc -  
 *topaz trabelsi - 
 *max metrikin - 
 ***/

namespace TDD_EX1
{
    public partial class Form1 : Form
    {
        public List<Book> books = new List<Book>();
        private Random random = new Random();
        private string[] titles = { "50 Shades of SCE", "Cant find Job after SCE", "The Dream of every SCE Student", "That Time i got Reincarnated as an SCE student" };
        private string[] authors = { "Israel Yakubov", "topaz trabelsi", "max metrikin" }; 
        private string[] categories = { "biography", "history", "novel"};

        public Form1()
        {
            InitializeComponent();
        }

        private void AddBookButton_Click(object sender, EventArgs e)
        {
            List<string> errorMessages = new List<string>();

            // Validate ISBN
            if (!int.TryParse(isbnTextBox.Text, out int isbn))
            {
                errorMessages.Add("Please enter a valid integer for ISBN.");
            }
            else if (isbn < 1000000)
            {
                errorMessages.Add("Please enter a valid integer for ISBN. (must be greater than 7 digits)");
            }

            // Validate Year
            if (!int.TryParse(yearTextBox.Text, out int year))
            {
                errorMessages.Add("Please enter a valid integer for Year.");
            }
            else if (year < 1900 || year > DateTime.Now.Year)
            {
                errorMessages.Add("Please enter a valid year (greater than 1900).");
            }

            // Validate Title
            string title = titleTextBox.Text;
            if (string.IsNullOrWhiteSpace(title))
            {
                errorMessages.Add("Title cannot be empty.");
            }

            // Validate Author
            string author = authorTextBox.Text;
            if (string.IsNullOrWhiteSpace(author))
            {
                errorMessages.Add("Author cannot be empty.");
            }

            // Validate Category
            string category = categoryTextBox.Text;
            if (string.IsNullOrWhiteSpace(category) || !categories.Contains(category.Trim().ToLower()))
            {
                errorMessages.Add("Please enter a valid category (e.g., Biography, History, Novel).");
            }

            // Check for existing ISBN
            if (errorMessages.Count == 0)
            {
                var existingBook = books.FirstOrDefault(b => b.ISBN == isbn);
                if (existingBook != null)
                {
                    errorMessages.Add("A book with this ISBN already exists.");
                }
            }

            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join("\n", errorMessages));
            }
            else
            {
                string status = comboBox1.SelectedItem.ToString();
                AddBookManually(isbn, year, title, author, category, status);
            }
        }



        private void GenerateBooksButton_Click(object sender, EventArgs e)
        {
            GenerateRandomBooks(10000);
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        public void AddBookManually(int isbn, int year, string title, string author, string category, string status)
        {
            Book book = new Book
            {
                ISBN = isbn,
                Title = title.Trim(),
                Author = author.Trim(),
                Year = year,
                Category = category.Trim(),
                Status = status
            };
            books.Add(book);
            DisplayBooks();
        }

        public void GenerateRandomBooks(int count)
        {
            try
            {
                int generatedCount = 0;
                while (generatedCount < count)
                {
                    int isbn = GenerateRandomISBN();
                    if (books.Any(b => b.ISBN == isbn)) continue;
                    Book book = new Book
                    {
                        ISBN = isbn,
                        Title = titles[random.Next(titles.Length)],
                        Author = authors[random.Next(authors.Length)],
                        Year = random.Next(1900, 2024),
                        Category = categories[random.Next(categories.Length)],
                        Status = random.Next(2) == 0 ? "Available" : "Borrowed"
                    };
                    books.Add(book);
                    generatedCount++;
                }
                DisplayBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int GenerateRandomISBN()
        {
            return random.Next(1000000, 10000000);
        }

        public void DisplayBooks()
        {
            booksDataGridView.DataSource = null;
            booksDataGridView.DataSource = books;
        }

        //bubble sort (not in use)
        public List<Book> BubbleSortBooksByYearDescending(List<Book> unsortedBooks)
        {
            var books = new List<Book>(unsortedBooks);
            int n = books.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (books[j].Year < books[j + 1].Year)
                    {
                        Book temp = books[j];
                        books[j] = books[j + 1];
                        books[j + 1] = temp;
                    }
                }
            }
            return books;
        }
        public List<Book> MergeSortBooksByYearDescending(List<Book> books)
        {
            if (books.Count <= 1)
                return books;
            int middleIndex = books.Count / 2;
            var leftHalf = books.GetRange(0, middleIndex);
            var rightHalf = books.GetRange(middleIndex, books.Count - middleIndex);
            leftHalf = MergeSortBooksByYearDescending(leftHalf);
            rightHalf = MergeSortBooksByYearDescending(rightHalf);
            return Merge(leftHalf, rightHalf);
        }
        private List<Book> Merge(List<Book> left, List<Book> right)
        {
            List<Book> result = new List<Book>();
            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0].Year > right[0].Year)
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }
            result.AddRange(left);
            result.AddRange(right);
            return result;
        }

        public void GenerateReport()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //var sortedBooks = BubbleSortBooksByYearDescending(books); bubble sort (not in use)...
                var sortedBooks = MergeSortBooksByYearDescending(books);
                stopwatch.Stop(); 

                int totalBooks = sortedBooks.Count;
                int availableBooks = sortedBooks.Count(b => b.Status == "Available");
                double averageYear = sortedBooks.Average(b => b.Year);

                MessageBox.Show($"Total Books: {totalBooks}\nAvailable Books: {availableBooks}\nAverage Year: {averageYear:F2}\nSort Time: {stopwatch.ElapsedMilliseconds} ms / {(float)stopwatch.ElapsedMilliseconds/1000.0}seconds");
                booksDataGridView.DataSource = sortedBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("Available");
            this.comboBox1.Items.Add("Borrowed");
            this.comboBox1.SelectedIndex = 0;
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void booksDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void isbnTextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
    // Boook class in here , get and set:
    public class Book
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
    }
}
