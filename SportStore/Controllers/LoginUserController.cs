using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Controllers
{
    public class LoginUserController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(Customer _user)
        {
            var check = db.Customers.Where(s => s.NameCus == _user.NameCus && s.PassCus == _user.PassCus).FirstOrDefault();
            if (check == null)//   login sai thong tin
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = _user.IDCus;
                Session["NameUser"] = _user.NameCus;
                Session["PasswordUser"] = _user.PassCus;
                return RedirectToAction("Index", "CustomerProducts");
            }
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(AdminUser _user)
        {
            var check = db.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)//   login sai thong tin
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("LoginAdmin");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = _user.ID;
                Session["NameUser"] = _user.NameUser;
                Session["PasswordUser"] = _user.PasswordUser;
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(Customer _cus)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.Customers.Where(s => s.NameCus == _cus.NameCus).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(_cus);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "ID đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "CustomerProducts");
        }
    }
}