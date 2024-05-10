using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Controllers
{
    public class OrderProductsController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: OrderProducts
        public ActionResult Index()
        {
            var orderproes = db.OrderProes.ToList();
            return View(orderproes);
        }

        public ActionResult Details(int id)
        {
            return View(db.OrderProes.Where(s => s.ID == id).FirstOrDefault());
        }
    }
}
