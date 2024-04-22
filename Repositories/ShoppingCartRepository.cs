using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using BrazilBlend.Data;
using BrazilBlend.Models;

namespace BrazilBlend.Repositoris;

public interface IShoppingCartRepository
{
    Task<int> AddItem(int productId, int quantity);
    Task<int> RemoveItem(int productId);
    Task<ShoppingCart> GetShoppingCart();
}

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public ShoppingCartRepository(ApplicationDbContext context, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    /// <summary>
    /// Add an item to the shopping cart.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public async Task<int> AddItem(int productId, int quantity)
    {
        using var transaction = _context.Database.BeginTransaction();
        string userId = GetUserId();

        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Invalid User ID.");
            }

            var shoppingCart = await GetUserActiveShoppingCart(userId);

            if (shoppingCart is null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                    IsActive = true
                };

                _context.ShoppingCart.Add(shoppingCart);
            }

            _context.SaveChanges();

            var cartItem = _context.CartItem.FirstOrDefault(x => x.ShoppingCartId == shoppingCart.Id && x.ProductId == productId);

            if (cartItem is not null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ShoppingCartId = shoppingCart.Id
                };

                _context.CartItem.Add(cartItem);
            }

            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return await GetCartItemsCount(userId);
    }

    /// <summary>
    /// Remove an item from the shopping cart.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async Task<int> RemoveItem(int productId)
    {
        string userId = GetUserId();

        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Invalid User ID.");
            }

            var shoppingCart = await GetUserActiveShoppingCart(userId);

            if (shoppingCart is null)
            {
                throw new Exception("Shopping Cart is empty.");
            }

            var cartItem = _context.CartItem.FirstOrDefault(x => x.ShoppingCartId == shoppingCart.Id && x.ProductId == productId);
            if (cartItem is null)
            {
                throw new Exception("Cart Item is empty.");
            }
            else if (cartItem.Quantity == 1)
            {
                _context.CartItem.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity--;
            }

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return await GetCartItemsCount(userId);
    }

    /// <summary>
    /// Get Shopping Cart List.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ShoppingCart> GetShoppingCart()
    {
        string userId = GetUserId();

        if (userId is null)
        {
            throw new Exception("Invalid User ID.");
        }

        var shoppingCart = await GetUserActiveShoppingCart(userId);

        if (shoppingCart is null)
        {
            shoppingCart = new ShoppingCart
            {
                UserId = userId,
                IsActive = true
            };

            _context.ShoppingCart.Add(shoppingCart);
        }

        _context.SaveChanges();

        return await _context.ShoppingCart
                                    .Include(x => x.CartItems)
                                        .ThenInclude(x => x.Product)
                                            .ThenInclude(x => x.Category)
                                    .Include(x => x.CartItems)
                                        .ThenInclude(x => x.Product)
                                            .ThenInclude(x => x.Brand)
                                    .Where(x => x.UserId == userId).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get existing shopping cart for the user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private async Task<ShoppingCart> GetUserActiveShoppingCart(string userId)
    {
        return await _context.ShoppingCart.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    private string GetUserId()
    {
        ClaimsPrincipal claimsPrincipal = _contextAccessor.HttpContext.User;
        return _userManager.GetUserId(claimsPrincipal);
    }

    private async Task<int> GetCartItemsCount(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            userId = GetUserId();
        }

        var data = await (from shoppingCart in _context.ShoppingCart 
                            join cartItem in _context.CartItem 
                            on shoppingCart.Id equals cartItem.ShoppingCartId 
                            select new { cartItem.Id }
                        ).ToListAsync();

        return data.Count;
    }
}