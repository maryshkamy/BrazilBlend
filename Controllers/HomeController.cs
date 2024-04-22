using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BrazilBlend.Data;
using BrazilBlend.ViewModels;

namespace BrazilBlend.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public HomeController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        var categories = await _context.Category
            .Include(c => c.Products)
            .ToListAsync();
        return View(categories);
    }

    // GET: Products/Image
    [HttpGet]
    public async Task<IActionResult> GetImage(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID");
        }

        var product = await _context.Product.FindAsync(id);

        if (product is null)
        {
            return NotFound("Product not found");
        }

        if (product.Image is not null && product.Image.Length > 0)
        {
            return File(product.Image, "image/jpeg");
        }
        else
        {
            string defaultImagePath = Path.Combine(_environment.WebRootPath, "images", "Icon.png");
            byte[] defaultImageBytes = await System.IO.File.ReadAllBytesAsync(defaultImagePath);

            return File(defaultImageBytes, "image/jpeg");
        }
    }

    // GET: Products/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Product == null)
        {
            return NotFound();
        }

        var product = await _context.Product
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}