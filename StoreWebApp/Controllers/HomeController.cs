using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreWebApp.Models;
using StoreWebApp.ViewModels;

namespace StoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Admin(int? category)
        {
            IndexViewModel model = new IndexViewModel();
            List<Category> categories = await db.Categories.ToListAsync();
            categories.Insert(0, new Category { Id = 0, Name = "Все категории" });
            model.Categories = categories;
            IQueryable<Product> products = db.Products.Include(p => p.Category);

            if (category != null && category != 0)
                products = products.Where(p => p.IdCategory == category);
            model.Products = products.ToList();
            model.SelectCategories = new SelectList(categories, "Id", "Name");
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Catalog(int? category)
        {
            IndexViewModel model = new IndexViewModel();
            List<Category> categories = await db.Categories.ToListAsync();
            categories.Insert(0, new Category { Id = 0, Name = "Все категории" });
            model.Categories = categories;
            IQueryable<Product> products = db.Products.Include(p => p.Category);

            if (category != null && category != 0)
                products = products.Where(p => p.IdCategory == category);
            model.Products = products.ToList();
            model.SelectCategories = new SelectList(categories, "Id", "Name");
            return View(model);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Create()
        {
            List<SelectListItem> categories = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Admin");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

                /*IndexViewModel model = new IndexViewModel();
                List<Category> categories = await db.Categories.ToListAsync();
                model.Categories = categories;
                IQueryable<Product> products = db.Products.Include(p => p.Category);*/

                ProductViewModel model = new ProductViewModel();
                model.Product = product;
                List<Product> recommendedList = new List<Product>();
                Product product1 = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.IdRecommendation1);
                recommendedList.Add(product1);
                Product product2 = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.IdRecommendation2);
                recommendedList.Add(product2);
                Product product3 = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.IdRecommendation3);
                recommendedList.Add(product3);
                model.RecommendedProducts = recommendedList;

                if (product != null)
                    return View(model);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> categories = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.Categories = categories;

            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Admin");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product product = new Product { Id = id.Value };
                db.Entry(product).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Admin");
            }
            return NotFound();
        }



        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Admin");
        }
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id != null)
            {
                Category category = await db.Categories.FirstOrDefaultAsync(p => p.Id == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Admin");
        }
        [HttpGet]
        [ActionName("DeleteCategory")]
        public async Task<IActionResult> ConfirmDeleteCategory(int? id)
        {
            if (id != null)
            {
                Category category = await db.Categories.FirstOrDefaultAsync(p => p.Id == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id != null)
            {
                Category category = new Category { Id = id.Value };
                db.Entry(category).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Admin");
            }
            return NotFound();
        }




    }

}
