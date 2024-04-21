using System.ComponentModel.DataAnnotations;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Shopping Cart</c> models the shopping cart information.
/// </summary>
public class ShoppingCart
{
    /// <summary>
    /// Reports the Shopping Cart's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Shopping Cart's User ID owner.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Reports the Shopping Cart's status.
    /// </summary>
    public bool IsActive { get; set; } = false;
}