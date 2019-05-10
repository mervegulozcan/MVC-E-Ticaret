using proje_eticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje_eticaret.Controllers
{
    public class StoreController : Controller
    {
        EticaretContext db = new EticaretContext();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Book(string Tur)
        {
            if(Tur==null)
            {
                List<Book> bk = db.Books.ToList();
                return View(bk);
            }
            else
            {
                List<Book> bk = db.Books.Where(x => x.Genre == Tur).ToList();
                return View(bk);
            }
            
        }
        public ActionResult Journal(string Tur)
        {
            if(Tur==null)
            {
                List<Journal> j = db.Journals.ToList();
                return View(j);
            }
            else
            {
                List<Journal> j = db.Journals.Where(x => x.Genre == Tur).ToList();
                return View(j);
            }
            
        }
        public ActionResult Film(string Tur)
        {
            if(Tur==null)
            {
                List<Film> flm = db.Films.ToList();
                return View(flm);
            }
            else
            {
                List<Film> flm = db.Films.Where(x => x.Genre == Tur).ToList();
                return View(flm);
            }
           
        }
        public ActionResult Music(string Tur)
        {
            if(Tur==null)
            {
                List<Music> m = db.Musics.ToList();
                return View(m);
            }
            else
            {
                List<Music> m = db.Musics.Where(x => x.Genre == Tur).ToList();
                return View(m);
            }
            
        }
        public ActionResult ProductFilmDetail()
        {
            int pID = Convert.ToInt32(Request.QueryString["id"]);
            Film f = db.Films.FirstOrDefault(x => x.ProductID == pID);
            return View(f);
        }
        public ActionResult ProductBookDetail()
        {
            int pID = Convert.ToInt32(Request.QueryString["id"]);
            Book b = db.Books.FirstOrDefault(x => x.ProductID == pID);
            return View(b);
        }
        public ActionResult ProductJournalDetail()
        {
            int pID = Convert.ToInt32(Request.QueryString["id"]);
            Journal j = db.Journals.FirstOrDefault(x => x.ProductID == pID);
            return View(j);
        }
        public ActionResult ProductMusicDetail()
        {
            int pID = Convert.ToInt32(Request.QueryString["id"]);
            Music m = db.Musics.FirstOrDefault(x => x.ProductID == pID);
            return View(m);
        }

    }
}