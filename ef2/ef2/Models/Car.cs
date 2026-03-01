namespace ef2.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Make), nameof(Model), IsUnique = true)]
public class Car
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Make { get; set; }

    [Required]
    public string Model { get; set; }

    [Range(1900, 2100)]
    public int Year { get; set; }

    public string? Color { get; set; }

    public bool IsDeleted { get; set; }

    public int DealerId { get; set; }
    public virtual Dealer Dealer { get; set; }

    public virtual ICollection<CarOrder> CarOrders { get; set; }
}
