using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrazilBlend.Data;
using BrazilBlend.Models;
using BrazilBlend.ViewModels;

namespace BrazilBlend.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Product.Include(p => p.Brand).Include(p => p.Category);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Products/Images
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

    // GET: Products/Details/5
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

    // GET: Products/Create
    public IActionResult Create()
    {
        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name");
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
        return View();
    }

    // POST: Products/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                Product product = new()
                {
                    Id = productViewModel.Id,
                    Name = productViewModel.Name,
                    BrandId = productViewModel.BrandId,
                    Price = productViewModel.Price,
                    Quantity = productViewModel.Quantity,
                    Image = null,
                    CategoryId = productViewModel.CategoryId
                };

                if (productViewModel.Image != null && productViewModel.Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await productViewModel.Image.CopyToAsync(memoryStream);
                        product.Image = memoryStream.ToArray();
                    }
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the product. " + ex.Message);
                return View(productViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", productViewModel.BrandId);
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", productViewModel.CategoryId);

        return View(productViewModel);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Product == null)
        {
            return NotFound();
        }

        var product = await _context.Product.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        ProductViewModel productViewModel = new()
        {
            Id = product.Id,
            Name = product.Name,
            BrandId = product.BrandId,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId
        };

        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", product.BrandId);
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);

        return View(productViewModel);
    }

    // POST: Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
    {
        if (id != productViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Name = productViewModel.Name;
                product.BrandId = productViewModel.BrandId;
                product.Price = productViewModel.Price;
                product.Quantity = productViewModel.Quantity;
                product.CategoryId = productViewModel.CategoryId;

                if (productViewModel.Image != null && productViewModel.Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await productViewModel.Image.CopyToAsync(memoryStream);
                        product.Image = memoryStream.ToArray();
                    }
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(productViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", productViewModel.BrandId);
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", productViewModel.CategoryId);

        return View(productViewModel);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null && _context.Product == null)
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

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Product.FindAsync(id);

        if (product != null)
        {
            _context.Product.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Product.Any(e => e.Id == id);
    }
}