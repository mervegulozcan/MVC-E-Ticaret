using proje_eticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje_eticaret.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        //public HomeController()
        //{
        //    if(üye girdiyse)
        //        {
        //        ViewBag.durum="üye girişi"
        //    }
        //}

        EticaretContext db = new EticaretContext();

        public ActionResult Index()
        {
            ViewBag.bookList = db.Books.Take(4).ToList();
            ViewBag.movieList=db.Films.Take(4).ToList();
            ViewBag.journalList = db.Journals.Take(4).ToList();
            ViewBag.musicList = db.Musics.Take(4).ToList();
            return View();
        }


    }
}