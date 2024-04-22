using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrazilBlend.ViewModels;

/// <summary>
/// Class <c>Checkout View Model</c> models the checkout information.
/// </summary>
public class CheckoutViewModel
{
    /// <summary>
    /// Reports the Checkout's Shopping Cart ID.
    /// </summary>
    public int ShoppingCartId { get; set; }

    /// <summary>
    /// Reports the Checkout's First Name.
    /// </summary>
    [Required]
    [DisplayName("First Name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Reports the Checkout's Last Name.
    /// </summary>
    [Required]
    [DisplayName("Last Name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Reports the Checkout's ASddress.
    /// </summary>
    [Required]
    public string? Address { get; set; }

    /// <summary>
    /// Reports the Checkout's Country.
    /// </summary>
    [Required]
    public string? Country { get; set; }

    /// <summary>
    /// Reports the Checkout's Province.
    /// </summary>
    [Required]
    public string? Province { get; set; }

    /// <summary>
    /// Reports the Checkout's Postal Code.
    /// </summary>
    [Required]
    [DisplayName("Postal Code")]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Reports the Checkout's Credit Card Name.
    /// </summary>
    [Required]
    [DisplayName("Credit Card Name")]
    public string? CreditCardName { get; set; }

    /// <summary>
    /// Reports the Checkout's Credit Card Number.
    /// </summary>
    [Required]
    [DisplayName("Credit Card Number")]
    public string? CreditCardNumber { get; set; }

    /// <summary>
    /// Reports the Checkout's Credit Card Expiration.
    /// </summary>
    [Required]
    [DisplayName("Credit Card Expiration")]
    public string? CreditCardExpiration { get; set; }

    /// <summary>
    /// Reports the Checkout's Credit Card CVV.
    /// </summary>
    [Required]
    [DisplayName("Credit Card CVV")]
    public string? CreditCardCVV { get; set; }
}