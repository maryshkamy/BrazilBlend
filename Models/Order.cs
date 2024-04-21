using System.ComponentModel.DataAnnotations;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Order</c> models the order information.
/// </summary>
public class Order
{
    /// <summary>
    /// Reports the Order's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Order's User ID.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Reports the Order's Status ID.
    /// </summary>
    [Required]
    public int OrderStatusId { get; set; }

    /// <summary>
    /// Reports the Order's Is Active status.
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Reports the Order's Created Date.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Reports the Order's lits of items.
    /// </summary>
    public List<OrderItem> Items { get; set; }

    /// <summary>
    /// Navigation property for the related Order Status entity.
    /// </summary>
    public virtual OrderStatus? OrderStatus { get; set; }

    /// <summary>
    /// Constructor initialize the Order Items collection.
    /// </summary>
    public Order()
    {
        Items = [];
    }
}