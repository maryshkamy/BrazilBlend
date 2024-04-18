using System.ComponentModel.DataAnnotations;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Brand</c> models the brand information.
/// </summary>
public class Brand {
    /// <summary>
    /// Reports the Brand's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Brand's Name.
    /// </summary>
    [Required]
    public string? Name { get; set; }

    /// <summary>
    /// Reports the Brand's products collection.
    /// </summary>
    public virtual List<Product> Products { get; set; }

    /// <summary>
    /// Constructor initialize the Products collection.
    /// </summary>
    public Brand() {
        Products = [];
    }
}