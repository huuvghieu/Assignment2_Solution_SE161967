using eBookStore.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Context
{
    public class EBookStoreDbContext : DbContext
    {
        public EBookStoreDbContext()
        {

        }
        public EBookStoreDbContext(DbContextOptions<EBookStoreDbContext> options) : base(options)
        {

        }

        #region Dbset
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<BookAuthor> BookAuthor { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId});

            #region RoleData
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Description = "Admin"
                },
                new Role
                {
                    RoleId = 2,
                    Description = "Member"
                }
                );
            #endregion

            #region PublisherData
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher
                {
                    PublisherId = 1,
                    PublisherName = "Nha Nam",
                    City = "HCM",
                    State = "Active",
                    Country = "Viet Nam",
                },
                new Publisher
                {
                    PublisherId = 2,
                    PublisherName = "Kinh Dong",
                    City = "Ha Noi",
                    State = "Active",
                    Country = "Viet Nam",
                },
                new Publisher
                {
                    PublisherId = 3,
                    PublisherName = "Hai Dang",
                    City = "HCM",
                    State = "Active",
                    Country = "Viet Nam",
                }

                );
            #endregion

            #region UserData
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    EmailAddres = "hieu@gmail.com",
                    Password = "1",
                    Source = "FPT",
                    FirstName = "Vuong",
                    MiddleName = "Huu",
                    LastName = "Hieu",
                    RoleId = 2,
                    PublisherId = 1,
                    HireDate = DateTime.Now,
                },
                new User
                {
                    UserId = 2,
                    EmailAddres = "trung@gmail.com",
                    Password = "1",
                    Source = "FPT",
                    FirstName = "Vuong",
                    MiddleName = "Huu",
                    LastName = "Trung",
                    RoleId = 2,
                    PublisherId = 2,
                    HireDate = DateTime.Now,
                },
                new User
                {
                    UserId = 3,
                    EmailAddres = "khang@gmail.com",
                    Password = "1",
                    Source = "FPT",
                    FirstName = "Vuong",
                    MiddleName = "Huu",
                    LastName = "Khang",
                    RoleId = 2,
                    PublisherId = 2,
                    HireDate = DateTime.Now,
                }
                );
            #endregion

            #region BookData
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Truyen Kieu",
                    PublishserId = 1,
                    Type = "Van hoc",
                    Price = 54000,
                    Advance = 100000,
                    Royalty = 150000,
                    YTDSale = 50,
                    Note = "Van hoc co dien Viet Nam",
                    PublishedDate = DateTime.Now,
                },
                new Book
                {
                    BookId = 2,
                    Title = "Tam ly hoc ve tien",
                    PublishserId = 2,
                    Type = "Tam ly",
                    Price = 132000,
                    Advance = 200000,
                    Royalty = 250000,
                    YTDSale = 40,
                    Note = "Tam ly hoc",
                    PublishedDate = DateTime.Now,
                },
                new Book
                {
                    BookId = 3,
                    Title = "Cay cam ngot cua toi",
                    PublishserId = 3,
                    Type = "Van hoc",
                    Price = 81000,
                    Advance = 110000,
                    Royalty = 140000,
                    YTDSale = 45,
                    Note = "Van hoc co dien",
                    PublishedDate = DateTime.Now,
                },
                new Book
                {
                    BookId = 4,
                    Title = "Nhung Cuoc Phieu Luu Cua Huckleberry Finn",
                    PublishserId = 2,
                    Type = "Van hoc",
                    Price = 83000,
                    Advance = 130000,
                    Royalty = 180000,
                    YTDSale = 60,
                    Note = "Van hoc co dien nuoc ngoai",
                    PublishedDate = DateTime.Now,
                },
                new Book
                {
                    BookId = 5,
                    Title = "Totto-chan Ben Cua So",
                    PublishserId = 1,
                    Type = "Van hoc",
                    Price = 98000,
                    Advance = 140000,
                    Royalty = 194000,
                    YTDSale = 78,
                    Note = "Van hoc co dien nuoc ngoai",
                    PublishedDate = DateTime.Now,
                }
                );
            #endregion

            #region AuthorData
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 1,
                    LastName = "Curry",
                    FirstName = "Stephen",
                    Phone = "0923999888",
                    Address = "Thu Duc District",
                    City = "Thu Duc",
                    State = "Active",
                    Zip = "71300",
                    EmailAddress = "curry@gmail.com",
                },
                new Author
                {
                    AuthorId = 2,
                    LastName = "Beal",
                    FirstName = "Braley",
                    Phone = "0923777888",
                    Address = "Tan Phu District",
                    City = "HCM",
                    State = "Active",
                    Zip = "76000",
                    EmailAddress = "beal@gmail.com",
                },
                new Author
                {
                    AuthorId = 3,
                    LastName = "Clarkson",
                    FirstName = "Jordan",
                    Phone = "0923666777",
                    Address = "Tan Binh District",
                    City = "HCM",
                    State = "Active",
                    Zip = "700915",
                    EmailAddress = "clarkson@gmail.com",
                }
                );
            #endregion
            #region BookAuthor
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor
                {
                    AuthorId = 1,
                    BookId = 1,
                    AuthorOrder = "Curry",
                    RoyaltyPercentage = 30
                },
                new BookAuthor
                {
                    AuthorId = 2,
                    BookId = 2,
                    AuthorOrder = "Beal",
                    RoyaltyPercentage = 35
                }
                );
            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strCon = config["ConnectionStrings:DefaultConnection"];
            return strCon;
        }
    }
}
