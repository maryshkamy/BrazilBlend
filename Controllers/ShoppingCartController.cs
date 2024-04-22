using Microsoft.AspNetCore.Mvc;

using BrazilBlend.Data;
using Microsoft.AspNetCore.Authorization;
using BrazilBlend.Repositoris;
using BrazilBlend.ViewModels;

namespace BrazilBlend.Controllers;

[Authorize]
public class ShoppingCartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartController(ApplicationDbContext context, IWebHostEnvironment environment, IShoppingCartRepository shoppingCartRepository)
    {
        _context = context;
        _environment = environment;
        _shoppingCartRepository = shoppingCartRepository;
    }

    // GET: ShoppingCart
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId is null) {
            return RedirectToAction("Login", "Account");
        }

        var shoppingCart = await _shoppingCartRepository.GetShoppingCart();

        if (shoppingCart is null) {

            return View();
        }

        return View(shoppingCart);
    }

    public async Task<IActionResult> AddItem(int productId, int quantity = 1, int redirect = 0)
    {
        var shoppingCart = await _shoppingCartRepository.AddItem(productId, quantity);
        if (redirect == 0)
            return Ok(shoppingCart);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveItem(int productId)
    {
        var shoppingCart = await _shoppingCartRepository.RemoveItem(productId);
        return RedirectToAction("Index");
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

    // GET: Checkout
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Order(CheckoutViewModel checkoutViewModel)
    {
        return View(checkoutViewModel);
    }

    private bool ShoppingCartExists(int id)
    {
        return _context.ShoppingCart.Any(e => e.Id == id);
    }
}
