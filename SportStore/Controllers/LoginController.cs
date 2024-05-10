﻿using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Controllers
{
    public class LoginController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: Login
        public ActionResult CusInfo()
        {
            return View();
        }
        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(Customer _user)
        {
            var check = db.Customers.Where(s => s.AccountCus == _user.AccountCus && s.PassCus == _user.PassCus).FirstOrDefault();
            var check1 = db.Customers.Where(s => s.AccountCus == _user.AccountCus && s.PassCus == _user.PassCus);
            if (check == null)//   login sai thong tin
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("loginuser");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = _user.IDCus;
                foreach (var i in check1)
                {
                    Session["NameUser"] = i.NameCus;
                    Session["Phone"] = i.PhoneCus;
                    Session["Email"] = i.EmailCus;
                    Session["Address"] = i.Address;
                    Session["Password"] = i.PassCus;
                    Session["Gender"] = i.Gender;

                }
                Session["PasswordUser"] = _user.PassCus;
                return RedirectToAction("Home", "Home");
            }
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(AdminUser _user)
        {
            var check = db.AdminUsers.Where(s => s.AccountUser == _user.AccountUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            var check1 = db.AdminUsers.Where(s => s.AccountUser == _user.AccountUser && s.PasswordUser == _user.PasswordUser);
            if (check == null)//   login sai thong tin
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("LoginAdmin");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = _user.ID;
                Session["PasswordUser"] = _user.PasswordUser;
                foreach (var i in check1)
                {
                    Session["NameUser"] = i.NameUser;

                }
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
                    return RedirectToAction("LoginUser");
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
            return RedirectToAction("Home", "home");
        }
    }
}