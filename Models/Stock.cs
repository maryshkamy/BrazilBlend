using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Stock</c> models the stock information.
/// </summary>
public class Stock {
    /// <summary>
    /// Reports the Stock's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Stock's Product ID.
    /// </summary>
    [Required]
    [ForeignKey("Product")]
    [DisplayName("Product")]
    public int ProductId { get; set; }

    /// <summary>
    /// Reports the Stock's Quantity.
    /// </summary>
    [Required]
    public int Quantity { get; set;}
}