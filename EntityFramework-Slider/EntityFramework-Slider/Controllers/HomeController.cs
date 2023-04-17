using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.Services.interfaces;
using EntityFramework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Diagnostics;

namespace EntityFramework_Slider.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        public HomeController(AppDbContext context,
                             IProductService productService,
                             IBasketService basketService)
        {
            _context = context;
            _basketService = basketService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _context.Sliders.ToListAsync();

            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();

            IEnumerable<Blog> blogs = await _context.Blogs.Where(m=>!m.SoftDelete).ToListAsync();

            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Product> products = await _context.Products.Include(m=>m.Images).ToListAsync();

            


            HomeVM model = new()
            {
                Sliders = sliders,
                SliderInfo = sliderInfo,
                Blogs = blogs,
                Categories = categories,
                Products = products,
                
            };

            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product? dbProduct = await _productService.GetById((int)id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> basket = _basketService.GetBasketDatas();

            

            BasketVM? existProduct = basket.FirstOrDefault(m=>m.Id == dbProduct.Id);


            _basketService.AddProductToBasket(existProduct, dbProduct, basket);

            

            int basketCount = basket.Sum(m => m.Count);

            return Ok(basketCount);
        }




        

    }


    
}