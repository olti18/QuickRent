using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickRent.Areas.Identity.Data;
using QuickRent.Models;

namespace QuickRent.Controllers
{
    public class CarsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        public readonly Data.QuickDbContext _context;
        public CarsController(Data.QuickDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var listofCars = _context.Cars.ToList();
            return View(listofCars);
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var car = _context.Cars.FirstOrDefault(car => car.Id == id);
            var viewModel = new CarsDetailsViewModel
            {
                Car = car,
                // Add other properties if needed
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Car car,IFormFile file)
        {
            try
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\car");

                    if (!string.IsNullOrEmpty(car.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, car.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    car.ImageUrl = @"\images\car\" + fileName;

                    _context.Cars.Add(car);
                    _context.SaveChangesAsync();
                    TempData["success"] = "Dog added Successfully";
                    return RedirectToAction(nameof(Index));


                }
            }
            catch
            {
                //ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Description", dog.BreedId);
                TempData["error"] = "Dog cannot been added";
                return View(car);
            }
            return View(car);
            /*
            _context.Cars.Add(car);
            _context.SaveChanges();
            return View();*/


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //var data = _context.Cars.FirstOrDefault(i => i.Id == id);
            var data = _context.Cars.Where(i => i.Id == id).FirstOrDefault();

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Car model) 
        { 
            var data = _context.Cars.Where(i => i.Id == model.Id).FirstOrDefault();
            if (data != null)
            {
                data.Description = model.Description;
                data.Fuel = model.Fuel;
                _context.SaveChanges();
            }
            return RedirectToAction("index");




        }
        public IActionResult Delete(int id)
        {
            var data = _context.Cars.Where(i=>i.Id == id).FirstOrDefault();
            _context.Cars.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        /*public async Task<IActionResult> Index()
        {
            var adoptionContext = _context.Cars.Include(d => d.Breeds);
            return View(await adoptionContext.ToListAsync());
        }*/
    }
}
