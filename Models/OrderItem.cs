using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Order Item</c> models the order item information.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Reports the Order Item's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Order Item's Order ID.
    /// </summary>
    [Required]
    [ForeignKey("Order")]
    [DisplayName("Order")]
    public int OrderId { get; set; }

    /// <summary>
    /// Reports the Order Item's Product ID.
    /// </summary>
    [Required]
    [ForeignKey("Product")]
    [DisplayName("Product")]
    public int ProductId { get; set; }

    /// <summary>
    /// Reports the Order Item's Qauntity.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Reports the Order Item's Price.
    /// </summary>
    [Required]
    public double Price { get; set; }

    /// <summary>
    /// Navigation property for the related Order entity.
    /// </summary>
    public virtual Order? Order { get; set; }

    /// <summary>
    /// Navigation property for the related Product entity.
    /// </summary>
    public virtual Product? Product { get; set; }
}