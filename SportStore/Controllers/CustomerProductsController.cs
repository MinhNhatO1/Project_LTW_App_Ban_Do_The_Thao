using PagedList;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Controllers
{
    public class CustomerProductsController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: CustomerProducts
        public ActionResult Index(string category, int? page, string SearchString, double min = double.MinValue, double max = double.MaxValue)
        {
            //Tìm kiếm chuỗi truy vấn theo category
            //var products = db.Products.Include(p => p.Category);

            //Tìm kiếm chuỗi truy vấn theo NamePro, nếu chuỗi truy vấn SearchString khác rỗng, null
            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    products = products.Where(s => s.NamePro.Contains(SearchString));
            //}

            //Tìm kiếm chuỗi truy vấn theo NamePro(SearchString)
            // Tìm kiếm chuỗi truy vấn theo đơn giá
            //Khai báo mỗi trang 4 sản phẩm
            //Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            //Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            //Nếu page = null thì đặt lại page là 1.
            //Trả về các product được phân trang theo kích thước và số trang.
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            if (page == null) page = 1;

            if (category == null)
            {
                var productList = db.Products.OrderByDescending(x => x.NamePro);
                return View(productList.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var productList = db.Products.OrderByDescending(x => x.NamePro).Where(p => p.Category == category);
                return View(productList.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

    }
}