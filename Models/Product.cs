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
    [ForeignKey("Brand")]
    [DisplayName("Brand")]
    public int BrandId { get; set;}

    /// <summary>
    /// Reports the Product's Price.
    /// </summary>
    [Required]
    public double Price { get; set; }

    /// <summary>
    /// Reports the Product's Quantity.
    /// </summary>
    [Required]
    public int Quantity { get; set;}

    /// <summary>
    /// Reports the Product's Category ID.
    /// </summary>
    [Required]
    [ForeignKey("Category")]
    [DisplayName("Category")]
    public int CategoryId { get; set; }

    /// <summary>
    /// Navigation property for the related Brand entity.
    /// </summary>
    public virtual Brand? Brand { get; set; }

    /// <summary>
    /// Navigation property for the related Category entity.
    /// </summary>
    public virtual Category? Category { get; set; }
}