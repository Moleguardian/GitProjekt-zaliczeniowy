using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Range(0.01, 10000)]
    public decimal Price { get; set; }

    public string Description { get; set; }
}
