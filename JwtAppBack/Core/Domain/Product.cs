using JwtAppBack.Core.Domain;

namespace JwtAppBack.Persistance.Core.Domain;

public class Product : BaseEntity
{
    
    public string? Name { get; set; }
    public int Stock { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
  
}
