using proje_eticaret.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using proje_eticaret.App_Classes;

namespace proje_eticaret.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        EticaretContext db = new EticaretContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookList()
        {
            List<Book> bookList = db.Books.ToList();
            return View(bookList);
        }

        public ActionResult AddBook(string pfrom)
        {
            ViewBag.pageFrom = pfrom;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Book b = db.Books.FirstOrDefault(x => x.ProductID == id);
            return View(b);
        }

        [HttpPost]
        public ActionResult AddBook([Bind(Include = "BookName,Author,Publisher,RelaseDate,ISBN,Genre,Stock,Price,Description")] Book b, HttpPostedFileBase picture)
        {
            if (picture == null)
            {
                return View();
            }
            byte[] asd = ConvertToBytes(picture);
            b.picture = asd;
            b.CategoryID = 1;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                db.Books.Add(b);
                db.SaveChanges();
                return RedirectToAction("BookList");
            }
            return View();

        }
        [HttpPost]
        public ActionResult DeleteBook(int id)
        {
            //int id = Convert.ToInt32(Request.QueryString["bID"]);
            Book b = db.Books.FirstOrDefault(x => x.ProductID == id);
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("BookList");

        }
        public ActionResult UpdateBook(int pID)
        {
            Book uBook = db.Books.FirstOrDefault(x => x.ProductID == pID);
            return View(uBook);
        }

        [HttpPost]
        public ActionResult UpdateBook([Bind(Include = "ProductID,BookName,Author,Publisher,RelaseDate,ISBN,Genre,Stock,Price,Description")] Book _book, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                Book b = db.Books.FirstOrDefault(x => x.ProductID == _book.ProductID);
            b.BookName = _book.BookName;
            b.CategoryID = 1;
            b.Author = _book.Author;
            b.Publisher = _book.Publisher;
            b.RelaseDate = _book.RelaseDate;
            b.ISBN = _book.ISBN;
            b.Genre = _book.Genre;
            b.Stock = _book.Stock;
            b.Price = _book.Price;
            b.Description = _book.Description;
            if (picture != null)
                {
                    b.picture = ConvertToBytes(picture);
                }
            db.SaveChanges();
            }
            return RedirectToAction("BookList");
        }


        public ActionResult FilmList()
        {
            List<Film> filmList = db.Films.ToList();
            return View(filmList);
        }

        public ActionResult AddFilm(string pfrom)
        {
            ViewBag.pageFrom = pfrom;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Film f = db.Films.FirstOrDefault(x => x.ProductID == id);
            return View(f);

        }

        [HttpPost]
        public ActionResult AddFilm([Bind(Include = "MovieName,Genre,RelaseDate,Director,Cast,IMDB,Stock,Price,Description")] Film f, HttpPostedFileBase Picture)
        {
            if (Picture == null)
                return View();

            f.Picture = ConvertToBytes(Picture);
            f.CategoryID = 2;
            if (ModelState.IsValid)
            {
                db.Films.Add(f);
                db.SaveChanges();
                return RedirectToAction("FilmList");
            }
            return View();

        }

        [HttpPost]
        public ActionResult DeleteFilm(int id)
        {
            //int id = Convert.ToInt32(Request.QueryString["bID"]);
            Film f = db.Films.FirstOrDefault(x => x.ProductID == id);
            db.Films.Remove(f);
            db.SaveChanges();
            return RedirectToAction("FilmList");

        }
        public ActionResult UpdateFilm(int pID)
        {
            Film uFilm = db.Films.FirstOrDefault(x => x.ProductID == pID);
            return View(uFilm);
        }
        [HttpPost]
        public ActionResult UpdateFilm([Bind(Include = "ProductID,MovieName,Genre,RelaseDate,Director,Cast,IMDB,Stock,Price,Description")] Film _film, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                Film f = db.Films.FirstOrDefault(x => x.ProductID == _film.ProductID);
                f.MovieName = _film.MovieName;
                f.Genre = _film.Genre;
                f.RelaseDate = _film.RelaseDate;
                f.Director = _film.Director;
                f.Cast = _film.Cast;
                f.IMDB = _film.IMDB;
                f.Stock = _film.Stock;
                f.Price = _film.Price;
                f.Description = _film.Description;
                if (Picture != null)
                {
                    f.Picture = ConvertToBytes(Picture);
                }
                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("FilmList");
        }

        public ActionResult JournalList()
        {
            List<Journal> journalList = db.Journals.ToList();
            return View(journalList);
        }

        public ActionResult AddJournal(string pfrom)
        {
            ViewBag.pageFrom = pfrom;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Journal j = db.Journals.FirstOrDefault(x => x.ProductID == id);
            return View(j);

        }

        [HttpPost]
        public ActionResult AddJournal([Bind(Include = "JournalName,Genre,Price,Stock,RelaseDate,Edition")] Journal j, HttpPostedFileBase Picture)
        {
            if (Picture == null)
                return View();

            j.picture = ConvertToBytes(Picture);
            j.CategoryID = 3;
            if (ModelState.IsValid)
            {
                db.Journals.Add(j);
                db.SaveChanges();
                return RedirectToAction("JournalList");
            }
            return View();

        }
        public ActionResult UpdateJournal(int pID)
        {
            Journal uJournal = db.Journals.FirstOrDefault(x => x.ProductID == pID);
            return View(uJournal);
        }
        [HttpPost]
        public ActionResult UpdateJournal([Bind(Include = "ProductID,JournalName,Genre,Price,Stock,RelaseDate,Edition")] Journal _journal, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                Journal j = db.Journals.FirstOrDefault(x => x.ProductID == _journal.ProductID);
            j.JournalName = _journal.JournalName;
            j.Genre = _journal.Genre;
            j.Price = _journal.Price;
            j.Stock = _journal.Stock;
            j.RelaseDate = _journal.RelaseDate;
            j.Edition = _journal.Edition;
                if (Picture != null)
                {
                    j.picture = ConvertToBytes(Picture);
                }
                db.SaveChanges();
            }
            return RedirectToAction("JournalList");
        }

        [HttpPost]
        public ActionResult DeleteJournal(int id)
        {
            //int id = Convert.ToInt32(Request.QueryString["bID"]);
            Journal j = db.Journals.FirstOrDefault(x => x.ProductID == id);
            db.Journals.Remove(j);
            db.SaveChanges();
            return RedirectToAction("JournalList");

        }

        public ActionResult MusicList()
        {
            List<Music> musicList = db.Musics.ToList();
            return View(musicList);
        }

        public ActionResult AddMusic(string pfrom)
        {
            ViewBag.pageFrom = pfrom;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Music m = db.Musics.FirstOrDefault(x => x.ProductID == id);
            return View(m);

        }

        [HttpPost]
        public ActionResult AddMusic([Bind(Include = "Artist,AlbumName,RelaseDate,Genre,Stock,Price,CompanyName")] Music m, HttpPostedFileBase Picture)
        {
            if (Picture == null)
                return View();

            m.picture = ConvertToBytes(Picture);
            m.CategoryID = 4;
            if (ModelState.IsValid)
            {
                db.Musics.Add(m);
                db.SaveChanges();
                return RedirectToAction("MusicList");
            }
            return View();
        }
        public ActionResult UpdateMusic(int pID)
        {
            Music uMusic = db.Musics.FirstOrDefault(x => x.ProductID == pID);
            return View(uMusic);
        }
        [HttpPost]
        public ActionResult UpdateMusic([Bind(Include = "ProductID,Artist,AlbumName,RelaseDate,Genre,Stock,Price,CompanyName")] Music _music, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                Music m = db.Musics.FirstOrDefault(x => x.ProductID == _music.ProductID);
                m.Artist = _music.Artist;
                m.AlbumName = _music.AlbumName;
                m.RelaseDate = _music.RelaseDate;
                m.Genre = _music.Genre;
                m.Stock = _music.Stock;
                m.Price = _music.Price;
                m.CompanyName = _music.CompanyName;
                if (Picture != null)
                {
                    m.picture = ConvertToBytes(Picture);
                }
                db.SaveChanges();
            }
            return RedirectToAction("MusicList");
        }

        [HttpPost]
        public ActionResult DeleteMusic(int id)
        {
            //int id = Convert.ToInt32(Request.QueryString["bID"]);
            Music m = db.Musics.FirstOrDefault(x => x.ProductID == id);
            db.Musics.Remove(m);
            db.SaveChanges();
            return RedirectToAction("MusicList");

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes(image.ContentLength);
            byte[] bytes = new byte[imageBytes.Length + 78];
            Array.Copy(imageBytes, 0, bytes, 78, imageBytes.Length);
            return bytes;
        }

        public ActionResult UserList()
        {
            List<Customer> customerList = db.Customers.ToList();

            return View(customerList);
        }
        public ActionResult OrderList()
        {
            List<OrderList> orderList = db.Orders.OrderByDescending(x => x.OrderDate).Select(x => new OrderList
            {
                OrderID = x.OrderID,
                CustomerID = x.CustomerID,
                CustomerName = x.Customer.Name,
                CustomerUserName = x.Customer.UserName,
                OrderDate = x.OrderDate,
                ShipperCompanyName = x.Shipper.CompanyName,
                ShipperDate = x.ShipperDate,
                TrackingNo = x.TrackingNo,
                DeliveryDate = x.DeliveryDate,
            }).ToList();
            return View(orderList);
        }

        public ActionResult AssignRole(string username, string message = null)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("Index");
            }
            MembershipUser user = Membership.GetUser(username);

            if (user == null)
            {
                return HttpNotFound();
            }

            string[] userRoles = Roles.GetRolesForUser(username);
            string[] allRoles = Roles.GetAllRoles();

            List<string> availableRoles = new List<string>();
            foreach (string role in allRoles)
            {
                if (!userRoles.Contains(role))
                {
                    availableRoles.Add(role);
                }
            }

            ViewBag.AvailableRoles = availableRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.UserName = username;
            ViewBag.Message = message;

            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(string username, List<string> addedRoles)
        {
            if (addedRoles == null)
                return RedirectToAction("AssignRole", new { username = username, message = "Önce rol seçiniz" });

            if (addedRoles.Count < 1)
                return RedirectToAction("AssignRole", new { username = username, message = "Hata" });

            Roles.AddUserToRoles(username, addedRoles.ToArray());

            return RedirectToAction("AssignRole", new { username = username, message = "Başarılı" });

        }

        [HttpPost]
        public string DeleteRole(string username, string removedRoles)
        {
            string[] removedRolesArray = removedRoles.Split(',');
            if (removedRolesArray.Length < 1 || string.IsNullOrWhiteSpace(removedRolesArray[0]))
            {
                return "Hata";
            }

            Roles.RemoveUserFromRoles(username, removedRolesArray);
            return "Başarılı";
        }
    }
}