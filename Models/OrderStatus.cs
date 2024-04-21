using System.ComponentModel.DataAnnotations;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Order Status</c> models the order status information.
/// </summary>
public class OrderStatus
{
    /// <summary>
    /// Reports the Order Status's ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Reports the Order Status's Name.
    /// </summary>
    [Required]
    public string? Name { get; set; }
}