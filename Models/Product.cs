using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Product</c> models the product information.
/// </summary>
public class Product {
    /// <summary>
    /// Reports the Product's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Products's Name.
    /// </summary>
    [Required]
    public string? Name { get; set; }

    /// <summary>
    /// Reports the Product's Brand.
    /// </summary>
    [Required]
    public string? Brand { get; set;}

    /// <summary>
    /// Reports the Product's Price.
    /// </summary>
    [Required]
    public double Price { get; set; }

    /// <summary>
    /// Reports the Product's Category ID.
    /// </summary>
    [Required]
    [ForeignKey("Category")]
    [DisplayName("Category")]
    public int CategoryId { get; set; }
}