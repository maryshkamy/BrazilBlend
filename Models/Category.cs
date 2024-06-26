using System.ComponentModel.DataAnnotations;

namespace BrazilBlend.Models;

/// <summary>
/// Class <c>Category</c> models the category information.
/// </summary>
public class Category {
    /// <summary>
    /// Reports the Category's ID.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Reports the Category's Name.
    /// </summary>
    [Required]
    public string? Name { get; set; }

    /// <summary>
    /// Reports the Category's products collection.
    /// </summary>
    public virtual List<Product> Products { get; set; }

    /// <summary>
    /// Constructor initialize the Products collection.
    /// </summary>
    public Category() {
        Products = [];
    }
}