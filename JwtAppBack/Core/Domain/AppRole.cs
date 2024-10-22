using JwtAppBack.Core.Domain;

namespace JwtAppBack.Persistance.Core.Domain;

public class AppRole : BaseEntity
{
    public AppRole(int id, string definition)
    {
        this.Id = id;
        this.Definition = definition;
    }
    public string? Definition { get; set; }

    public List<AppUser>? AppUsers { get; set; }
  
}
