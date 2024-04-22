using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Cart Item</c> models the cart item information.
/// </summary>
public class CartItem
{
    /// <summary>
    /// Reports the Cart Item's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Cart Item's Product ID.
    /// </summary>
    [Required]
    [ForeignKey("Product")]
    [DisplayName("Product")]
    public int ProductId { get; set; }

    /// <summary>
    /// Reports the Cart Item's Quantity.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Reports the Cart Item's Shopping Cart ID.
    /// </summary>
    [Required]
    [ForeignKey("ShoppingCart")]
    [DisplayName("ShoppingCart")]
    public int ShoppingCartId { get; set; }

    /// <summary>
    /// Navigation property for the related Product entity.
    /// </summary>
    public virtual Product? Product { get; set; }

    /// <summary>
    /// Navigation property for the related Shopping Cart entity.
    /// </summary>
    public virtual ShoppingCart? ShoppingCart{ get; set; }
}