using proje_eticaret.App_Classes;
using proje_eticaret.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace proje_eticaret.Controllers
{
    public class UserController : Controller
    {
        EticaretContext ctx = new EticaretContext();
        List<MyCart> cartList = new List<MyCart>();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUserAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserAccount(UserClass uc)
        {

            MembershipCreateStatus createStatus;
            Membership.CreateUser(uc.UserName, uc.Password, uc.Email, uc.PasswordQuestion, uc.PasswordAnswer, true, out createStatus);
            string messageStatus = "";

            switch (createStatus)
            {

                case MembershipCreateStatus.Success:

                    break;
                case MembershipCreateStatus.InvalidUserName:
                    messageStatus = "Geçersiz kullanıcı adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    messageStatus = "Geçersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    messageStatus = "Geçersiz gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    messageStatus = "Geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    messageStatus = "Geçersiz email";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    messageStatus = "Kullanılmış kullanıcı adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    messageStatus = "Kullanılmış email";
                    break;
                case MembershipCreateStatus.UserRejected:
                    messageStatus = "Engellenmiş kullanıcı";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    messageStatus = "Geçersiz kullanıcı anahtarı";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    messageStatus = "Kullanılmış kullanıcı anahtarı";
                    break;
                case MembershipCreateStatus.ProviderError:
                    messageStatus = "Üye yönetimi sağlayıcı hatası";
                    break;
                default:
                    break;
            }
            ViewBag.messageStatus = messageStatus;
            if (createStatus == MembershipCreateStatus.Success)
            {
                string[] addRole = { "üye" };
                Roles.AddUserToRoles(uc.UserName, addRole.ToArray());
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        public ActionResult LoginToSystem()
        {
            return View("Index");
        }
        [HttpPost]
        public ActionResult LoginToSystem(UserClass uc, string RememberMe)
        {
            bool validateResult = Membership.ValidateUser(uc.UserName, uc.Password);

            string[] userRoles = Roles.GetRolesForUser(uc.UserName);

            if (userRoles.Contains("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                if (validateResult == true)
                {
                    if (RememberMe == "on")
                    {
                        FormsAuthentication.RedirectFromLoginPage(uc.UserName, true);
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(uc.UserName, false);
                    }
                }
                else
                {
                    ViewBag.validateMessage = "Kullanıcı adı ya da parola hatalı";
                }
                return View("Index");
            }
        }
        public ActionResult LogoutToSystem()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginToSystem");
        }
        public ActionResult ResetPassword(string uname)
        {
            ViewBag.parolaname = uname;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(UserClass uc)
        {
            if (!User.Identity.IsAuthenticated)
            {
                try
                {
                    MembershipUser mu = Membership.GetUser(uc.UserName);

                    if (mu.PasswordQuestion == uc.PasswordQuestion)
                    {
                        string newPwd = mu.ResetPassword(uc.PasswordAnswer);
                        if (newPwd != null)
                        {
                            mu.ChangePassword(newPwd, uc.Password);
                        }
                        return RedirectToAction("LoginToSystem");
                    }
                    else
                    {
                        ViewBag.resetPasswordMessage = "Girilen bilgiler yanlıştır";
                        return View();
                    }
                }
                catch (Exception)
                {
                    ViewBag.resetPasswordMessage = "Girilen bilgiler yanlıştır";
                    return View();
                }

            }
            else
            {
                try
                {
                    MembershipUser mu = Membership.GetUser(User.Identity.Name);

                    if (mu.PasswordQuestion == uc.PasswordQuestion)
                    {
                        string newPwd = mu.ResetPassword(uc.PasswordAnswer);
                        if (newPwd != null)
                        {
                            mu.ChangePassword(newPwd, uc.Password);
                            return RedirectToAction("UserPanel");
                        }
                    }
                    else
                    {
                        ViewBag.resetPasswordMessage = "Girilen bilgiler yanlıştır";
                        return RedirectToAction("UserPanel");
                    }

                }
                catch (Exception)
                {
                    ViewBag.resetPasswordMessage = "Girilen bilgiler yanlıştır";
                    return RedirectToAction("UserPanel");
                }

            }
            return View();
        }
        public ActionResult PartialCartList()
        {
            cartList = (List<MyCart>)Session["CurrentCart"];
            return PartialView(cartList);
        }
        public ActionResult PartialCart()
        {
            cartList = (List<MyCart>)Session["CurrentCart"];
            return PartialView(cartList);
        }
        public ActionResult UserPanel()
        {
            string uName = Request.QueryString["uName"].ToString();
            List<Customer> cmList = ctx.Customers.ToList();
            var cmb = cmList.FirstOrDefault(x => x.UserName == uName);
            if (cmb != null)
            {
                return View(cmList);
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserPanel(Customer c)
        {
            Customer cst = ctx.Customers.FirstOrDefault(x => x.UserName == c.UserName);
            if (cst != null)
            {
                if (cst.UserName == c.UserName)
                {
                    cst.Name = c.Name;
                    cst.Email = c.Email;
                    cst.County = c.County;
                    cst.Address = c.Address;
                    cst.Phone = c.Phone;
                    ctx.SaveChanges();
                    return View();
                }
                return View();
            }
            else
            {
                ctx.Customers.Add(c);
                ctx.SaveChanges();
                return RedirectToAction("UserPanel");
            }

        }
        public ActionResult UserOrders()
        {
            string uName = Request.QueryString["uName"];
            //List<Customer> cmList = ctx.Customers.ToList();
            //var cmb = cmList.FirstOrDefault(x => x.UserName == uName);
            Customer c = ctx.Customers.FirstOrDefault(x => x.UserName == uName);
            List<Order> o = ctx.Orders.Where(x => x.CustomerID == c.CustomerID).ToList();

            if (o != null)
            {
                return View(o);
            }
            return View();
        }

        public ActionResult GetOrderDetail(int id)
        {
            List<OrderDetail> orderDetailList = ctx.OrderDetails.Where(x => x.OrderID == id).ToList();
            return View(orderDetailList);
        }

        public ActionResult UserCart()
        {
            string pName = Request.QueryString["name"];
            decimal pPrice = Convert.ToDecimal(Request.QueryString["price"]);
            MyCart mc = new MyCart();
            mc.ProductName = pName;
            mc.Price = pPrice;
            if (Session["CurrentCart"] != null)
            {
                cartList = (List<MyCart>)Session["CurrentCart"];
            }
            if (!cartList.Contains(mc) && (mc.ProductName != null))
            {
                cartList.Add(mc);
                Session["CurrentCart"] = cartList;
            }
            return View(cartList);
        }

        //public string UserCart(string pName)
        //{
        //    cartList = (List<MyCart>)Session["CurrentCart"];
        //    foreach (MyCart mc in cartList)
        //    {
        //        if (mc.ProductName == pName)
        //        {
        //            cartList.Remove(mc);
        //            Session["CurrentCart"] = cartList;
        //            break;
        //        }
        //    }
        //    return RedirectToAction("UserCart");
        //    return "b";
        //}

        //[HttpPost]
        //public string RemoveProductFromCart(string pName)
        //{
        //    cartList = (List<MyCart>)Session["CurrentCart"];
        //    foreach (MyCart mc in cartList)
        //    {
        //        if (mc.ProductName == pName)
        //        {
        //            cartList.Remove(mc);
        //            Session["CurrentCart"] = cartList;
        //            break;
        //        }
        //    }
        //    //return RedirectToAction("UserCart");
        //    return "b";
        //}
        [HttpPost]
        public string AddCart(string pName, decimal pPrice)
        {
            string messageAddCart = "";
            MyCart mc = new MyCart();
            mc.ProductName = pName;
            mc.Price = pPrice;
            if (Session["CurrentCart"] != null)
            {
                cartList = (List<MyCart>)Session["CurrentCart"];
            }
            if (!cartList.Contains(mc) && (mc.ProductName != null))
            {
                cartList.Add(mc);
                Session["CurrentCart"] = cartList;
            }
            messageAddCart = "Ürün sepete eklendi";
            return messageAddCart;
        }
        public void CreateOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                Random rnd = new Random();
                Order o = new Order();
                Customer c = ctx.Customers.FirstOrDefault(x => x.UserName == User.Identity.Name);
                o.CustomerID = c.CustomerID;
                o.OrderDate = DateTime.Today;
                o.DeliveryDate = DateTime.Today.AddDays(4);
                o.TrackingNo = rnd.Next(10000, 99999);
                o.ShipperID = 1;
                o.ShipperDate = DateTime.Today.AddDays(1);
                ctx.Orders.Add(o);
                ctx.SaveChanges();
                ViewBag.orderID = o.OrderID;
            }

        }

        public void CreateOrderDetails()
        {
            cartList = (List<MyCart>)Session["CurrentCart"];
            foreach (MyCart mc in cartList)
            {
                mc.OrderID = ViewBag.orderID;
                if (ctx.Books.FirstOrDefault(x => x.BookName == mc.ProductName) != null)
                {
                    Book b = ctx.Books.FirstOrDefault(x => x.BookName == mc.ProductName);
                    mc.ProductID = b.ProductID;
                    mc.CategoryID = b.CategoryID;
                    mc.Price = b.Price;
                }

                if (ctx.Films.FirstOrDefault(x => x.MovieName == mc.ProductName) != null)
                {
                    Film f = ctx.Films.FirstOrDefault(x => x.MovieName == mc.ProductName);
                    mc.ProductID = f.ProductID;
                    mc.CategoryID = f.CategoryID;
                    mc.Price = f.Price;
                }

                if (ctx.Journals.FirstOrDefault(x => x.JournalName == mc.ProductName) != null)
                {
                    Journal j = ctx.Journals.FirstOrDefault(x => x.JournalName == mc.ProductName);
                    mc.ProductID = j.ProductID;
                    mc.CategoryID = j.CategoryID;
                    mc.Price = j.Price;
                }

                if (ctx.Musics.FirstOrDefault(x => x.AlbumName == mc.ProductName) != null)
                {
                    Music m = ctx.Musics.FirstOrDefault(x => x.AlbumName == mc.ProductName);
                    mc.ProductID = m.ProductID;
                    mc.CategoryID = m.CategoryID;
                    mc.Price = m.Price;
                }
                OrderDetail od = new OrderDetail();
                od.OrderID = mc.OrderID;
                od.ProductID = mc.ProductID;
                od.CategoryID = mc.CategoryID;
                od.Price = mc.Price;
                od.Quantity = 1;
                ctx.OrderDetails.Add(od);
            }
            ctx.SaveChanges();

        }
        [HttpPost]
        public string OrderConfirm()
        {
            string mesaj = "";

            DbContextTransaction tran = ctx.Database.BeginTransaction();
            try
            {
                CreateOrder();
                CreateOrderDetails();
                tran.Commit();
                mesaj = "İşlem başarılı";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
                tran.Rollback();
            }
            return mesaj;
        }



        public ActionResult FavouriteList()
        {
            string uName = Request.QueryString["uName"].ToString();
            Customer c = ctx.Customers.FirstOrDefault(x => x.UserName == uName);

            List<Favourite> favouriteList = ctx.Favourites.Where(x => x.CustomerID == c.CustomerID).ToList();
            return View(favouriteList);
        }

        [HttpPost]
        public string FavouriteList(int pID, int cID)
        {

            string mesaj = string.Empty;
            try
            {
                Customer cu = ctx.Customers.FirstOrDefault(x => x.UserName == User.Identity.Name);
                Favourite f = new Favourite();
                f.CustomerID = cu.CustomerID;
                f.ProductID = pID;
                f.CategoryID = cID;
                ctx.Favourites.Add(f);
                ctx.SaveChanges();
                mesaj = "Ürün favori listesine eklendi";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
                mesaj = "ürün favori listesinde mevcut";
            }

            return mesaj;
        }
       [HttpPost]
       public string RemoveFromFavourite(int pID, int cID)
        {
            string mesaj = string.Empty;
            try
            {
                Customer cu = ctx.Customers.FirstOrDefault(x => x.UserName == User.Identity.Name);
                Favourite f = ctx.Favourites.FirstOrDefault(x => x.CustomerID == cu.CustomerID && x.CategoryID == cID && x.ProductID == pID);

                ctx.Favourites.Remove(f);
                ctx.SaveChanges();
                mesaj = "Ürün favori listesinden çıkartıldı";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }

            return mesaj;
        }
    }
}